using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class CambioChoferPresenter : BasePresenter<ICambioChoferView>
    {
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IChoferEstadoRepositorio _choferEstadoRepositorio;
        private Nomina _nominaActual;

        public CambioChoferPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            INominaRepositorio nominaRepositorio,
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IChoferEstadoRepositorio choferEstadoRepositorio
        ) : base(sesionService, navigationService)
        {
            _nominaRepositorio = nominaRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _choferRepositorio = choferRepositorio;
            _choferEstadoRepositorio = choferEstadoRepositorio;
        }

        public async Task InicializarAsync(int idNomina)
        {
            _nominaActual = await _nominaRepositorio.ObtenerPorIdAsync(idNomina);

            Unidad? unidad = await _unidadRepositorio.ObtenerPorUnidadIdAsync(_nominaActual.IdUnidad);

            List<Chofer> choferes = await _choferRepositorio.ObtenerTodosLosChoferesPorEmpresa(unidad.IdEmpresa);
            _view.CargarChoferes(choferes);
        }

        public async Task ChoferSeleccionadoCambioAsync()
        {
            // Llamar este método desde el evento de selección del grid de choferes
            int? idChofer = _view.IdChoferSeleccionado;
            if (idChofer.HasValue)
            {
                var francos = await _choferEstadoRepositorio.ObtenerPorChoferAsync(idChofer.Value);
                _view.CargarFrancos(francos);
            }
            else
            {
                _view.CargarFrancos(new List<NovedadesChoferesDto>());
            }
        }

        public async Task ConfirmarCambioChoferAsync()
        {
            int? idChofer = _view.IdChoferSeleccionado;
            if(idChofer == _nominaActual.IdChofer)
            {
                _view.MostrarMensaje("El chofer seleccionado es el mismo que ya está asignado a la unidad.");
                return;
            }
            int idUnidad = _nominaActual.IdUnidad;
            DateTime fecha = _view.FechaCambio;
            string? Observaciones = _view.Observacion;
            // Llamar SP (agregar método al repositorio)
            await _nominaRepositorio.CambiarChoferUnidadAsync(idChofer, idUnidad, fecha, Observaciones);

            // Registrar evento en NominaRegistro
            string nombre = _view.NombreChoferSeleccionado ?? "Sin chofer";
            string descripcion = $"{nombre} - {fecha:dd/MM/yyyy}";
            await _nominaRepositorio.RegistrarNominaAsync(_nominaActual.IdNomina, "Cambio Chofer", descripcion, _sesionService.IdUsuario);

            _view.MostrarMensaje("Cambio de chofer realizado correctamente.");
            _view.Cerrar();
        }

        public async Task BajarChoferAsync()
        {
            int idUnidad = _nominaActual.IdUnidad;
            DateTime fecha = _view.FechaCambio;
            // SP con null en idChofer para bajar chofer
            await _nominaRepositorio.CambiarChoferUnidadAsync(null, idUnidad, fecha, null);

            await _nominaRepositorio.RegistrarNominaAsync(
                _nominaActual.IdNomina,
                "Cambio Chofer",
                "Sin chofer - " + fecha.ToString("dd/MM/yyyy"),
                _sesionService.IdUsuario
            );

            _view.MostrarMensaje("Chofer dado de baja correctamente.");
            _view.Cerrar();
        }
    }
}