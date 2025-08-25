using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class AgregarEditarAlertaPresenter : BasePresenter<IAgregarEditarAlertaView>
    {
        private readonly IAlertaRepositorio _alertaRepositorio;

        private readonly ISemiRepositorio _semiRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;

        private readonly ITractorRepositorio _tractorRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        public Alerta? AlertaActual { get; private set; }

        public AgregarEditarAlertaPresenter(
            IAlertaRepositorio alertaRepositorio,
            ITractorRepositorio tractorRepositorio,
            IChoferRepositorio choferRepositorio,
            ISemiRepositorio semiRepositorio,
            IUnidadRepositorio unidadRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _alertaRepositorio = alertaRepositorio;
            _tractorRepositorio = tractorRepositorio;
            _semiRepositorio = semiRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _choferRepositorio = choferRepositorio;
        }

        public async Task InicializarAsync(int idAlerta)
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<Chofer> choferes = await _choferRepositorio.ObtenerTodosLosChoferes();
                List<Tractor> tractores = await _tractorRepositorio.ObtenerTodosLosTractores();
                List<Semi> semis = await _semiRepositorio.ObtenerTodosLosSemis();

                List<Chofer> choferesOrdenadas = choferes.OrderBy(c => c.NombreApellido).ToList();
                List<Tractor> tractorOrdenadas = tractores.OrderBy(c => c.Patente).ToList();
                List<Semi> semiOrdenadas = semis.OrderBy(c => c.Patente).ToList();

                _view.CargarTractores(tractorOrdenadas);
                _view.CargarChoferes(choferesOrdenadas);
                _view.CargarSemis(semiOrdenadas);
            });

            if (idAlerta != 0)
            {
                AlertaActual = await _alertaRepositorio.ObtenerPorIdAsync(idAlerta);
                _view.MostrarDatosAlerta(AlertaActual);
            }
        }

        public async Task GuardarAsync()
        {
            var alerta = new Alerta
            {
                IdAlerta = AlertaActual?.IdAlerta ?? 0,
                IdChofer = _view.IdChofer,
                IdTractor = _view.IdTractor,
                IdSemi = _view.IdSemi,
                Descripcion = _view.Descripcion
            };

            if (_view.IdTractor != null && _view.IdSemi != null)
            {
                int? idunidad = await _unidadRepositorio.ObtenerIdUnidadPorTractorAsync(_view.IdTractor.Value);
                Unidad unidad = await _unidadRepositorio.ObtenerPorUnidadIdAsync(idunidad.Value);
                if (unidad.IdSemi != _view.IdSemi)
                {
                    _view.MostrarMensaje("El semi es distinto al de la unidad.");
                    return;
                }
            }
            if (await ValidarAsync(alerta))
            {
                await EjecutarConCargaAsync(async () =>
                {
                    if (AlertaActual == null)
                    {
                        await _alertaRepositorio.AgregarAlertaAsync(alerta, _sesionService.IdUsuario);
                        _view.MostrarMensaje("Alerta Agregado Correctamente");
                    }
                    else
                    {
                        await _alertaRepositorio.ActualizarAlertaAsync(alerta, _sesionService.IdUsuario);
                        _view.MostrarMensaje("Alerta Actualizado Correctamente");
                    }
                    _view.Close();
                });
            }
        }
    }
}