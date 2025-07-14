using Core.Base;
using Core.Repositories;
using Core.Services;
using DevExpress.XtraEditors;
using GestionFlota.Views;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class NuevoProgramaPresenter : BasePresenter<INuevoProgramaView>
    {
        private readonly IAlertaRepositorio _alertaRepositorio;
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IDisponibilidadRepositorio _disponibilidadRepositorio;

        private Cupeo _cupeo;

        public NuevoProgramaPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IAlertaRepositorio alertaRepositorio,
            ILocacionRepositorio locacionRepositorio,
            IProductoRepositorio productoRepositorio,
            INominaRepositorio nominaRepositorio,
            IDisponibilidadRepositorio disponibilidadRepositorio,
            IProgramaRepositorio programaRepositorio)
            : base(sesionService, navigationService)
        {
            _alertaRepositorio = alertaRepositorio;
            _locacionRepositorio = locacionRepositorio;
            _productoRepositorio = productoRepositorio;
            _programaRepositorio = programaRepositorio;
            _disponibilidadRepositorio = disponibilidadRepositorio;
            _nominaRepositorio = nominaRepositorio;
        }

        public async Task InicializarAsync(Cupeo cupeo)
        {
            _cupeo = cupeo;

            var alertas = await _alertaRepositorio.ObtenerAlertasPorIdNominaAsync(cupeo.IdNomina);
            _view.MostrarAlertas(alertas);

            var origenes = await _locacionRepositorio.ObtenerTodasAsync();
            var destinos = await _locacionRepositorio.ObtenerTodasAsync();
            var productos = await _productoRepositorio.ObtenerTodosAsync();

            _view.CargarOrigenes(origenes, cupeo.IdOrigen);
            _view.CargarDestinos(destinos);
            _view.CargarProductos(productos);
        }

        public async Task GuardarAsync()
        {
            var programa = new Programa
            {
                IdDisponible = _cupeo.IdDisponible ?? 0,
                IdPedido = null,
                IdOrigen = _view.IdOrigenSeleccionado ?? 0,
                IdProducto = _view.IdProductoSeleccionado ?? 0,
                Cupo = _cupeo.Cupo ?? 0,
                AlbaranDespacho = int.TryParse(_view.Albaran, out var alb) ? alb : 0,
                PedidoOr = int.TryParse(_view.Pedido, out var ped) ? ped : 0,
                Observaciones = _view.Observaciones,
                IdProgramaEstado = 1, // Estado "Asignado"
                FechaCarga = _view.FechaCarga,
                FechaEntrega = _view.FechaEntrega,
                Extranjero = _view.Extranjero,  
            };

            if (_view.IdOrigenSeleccionado == _cupeo.IdOrigen)
            {
                programa.Cupo = _cupeo.Cupo ?? 0;
            }
            else
            {
                programa.Cupo = _view.Cupo.Value;
            }
            int idPrograma = await _programaRepositorio.InsertarProgramaRetornandoIdAsync(programa);

            // Si necesitás crear un tramo también:
            var tramo = new ProgramaTramo
            {
                IdPrograma = idPrograma,
                IdNomina = _cupeo.IdNomina,
                IdDestino = _view.IdDestinoSeleccionado ?? 0,
                FechaInicio = _view.FechaCarga,
                FechaFin = null
            };
            await _programaRepositorio.InsertarProgramaTramoAsync(tramo);

            _view.MostrarMensaje("Programa creado correctamente.");
            _view.Cerrar();
        }

        public async Task ActualizarCuposDisponiblesAsync(int idOrigen)
        {
            Disponible? disponible = await _disponibilidadRepositorio.ObtenerPorIdAsync(_cupeo.IdDisponible.Value);
            var cuposUsados = await _disponibilidadRepositorio.ObtenerCuposUsadosAsync(idOrigen, disponible.FechaDisponible);
            // Rango: 1..7, quitando los ya usados (y el del actual si se está editando)
            var cuposDisponibles = CalcularCuposDisponibles(cuposUsados);

            // Si edita y su propio cupo está en la lista usada, debe poder verlo
            if (_cupeo != null && !cuposDisponibles.Contains(_cupeo.Cupo.Value))
                cuposDisponibles.Add(_cupeo.Cupo.Value);

            _view.CargarCupos(cuposDisponibles.OrderBy(x => x).ToList());
        }

        public List<int> CalcularCuposDisponibles(List<int> cuposOcupados)
        {
            var disponibles = new List<int>();
            int cupo = 1;

            // Hacemos esto hasta tener 5 cupos disponibles
            while (disponibles.Count < 5)
            {
                if (!cuposOcupados.Contains(cupo))
                {
                    disponibles.Add(cupo);
                }
                cupo++;
            }
            return disponibles;
        }

        public async Task RegistrarComentarioAsync(string comentario)
        {
            await _nominaRepositorio.RegistrarNominaAsync(
                _cupeo.IdNomina, // o el disponible.IdNomina según contexto
                "Comentario",
                comentario,
                _sesionService.IdUsuario
            );
            _view.MostrarMensaje("Comentario registrado correctamente.");
        }

        public async Task CambiarChoferAsync()
        {
            await AbrirFormularioAsync<CambioChoferForm>(async form =>
            {
                await form._presenter.InicializarAsync(_cupeo.IdNomina);
            });
            _view.Cerrar();
        }

        public async Task CancelarDisponibleAsync()
        {
            // Si no está confirmado: cancelar disponible
            var disponible = await _disponibilidadRepositorio.ObtenerPorIdAsync(_cupeo.IdDisponible.Value);
            if (disponible == null)
            {
                _view.MostrarMensaje("No se encontró ningún disponible para la fecha seleccionada.");
                return;
            }

            var motivosDisp = await _disponibilidadRepositorio.ObtenerEstadosDeBajaAsync();
            var controlDisp = new MotivoBajaSelectorControl(motivosDisp);
            if (XtraDialog.Show(controlDisp, "Seleccione motivo de baja", MessageBoxButtons.OKCancel) == DialogResult.OK
                && controlDisp.MotivoSeleccionado.HasValue)
            {
                await CambiarEstadoDeBajaDisponibleAsync(disponible, controlDisp.MotivoSeleccionado.Value);
                _view.Cerrar();
            }
        }

        private async Task CambiarEstadoDeBajaDisponibleAsync(Disponible disponible, int idMotivo)
        {
            disponible.IdDisponibleEstado = idMotivo;
            await _disponibilidadRepositorio.ActualizarDisponibleAsync(disponible);

            // Log en NominaRegistro
            var motivo = await _disponibilidadRepositorio.ObtenerEstadoDeBajaPorIdAsync(idMotivo);
            await _nominaRepositorio.RegistrarNominaAsync(
                disponible.IdNomina,
                "Cancela Disponible",
                motivo?.Descripcion ?? "Sin motivo",
                _sesionService.IdUsuario
            );
        }

        //public async void AbrirNovedades(string tipoNovedad, string tipoPermiso)
        //{
        //    await AbrirFormularioConPermisosAsync<NovedadesForm>(tipoPermiso, async form =>
        //    {
        //        await form._presenter.CargarNovedadesAsync(false, tipoNovedad);
        //    });
        //}
    }
}