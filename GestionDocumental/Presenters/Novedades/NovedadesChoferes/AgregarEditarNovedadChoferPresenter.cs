using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionDocumental.Views;
using Shared.Models;

namespace GestionDocumental.Presenters
{
    public class AgregarEditarNovedadChoferPresenter : BasePresenter<IAgregarEditarNovedadChoferView>
    {
        private readonly IChoferEstadoRepositorio _choferEstadoRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        public NovedadesChoferesDto? NovedadActual { get; private set; }

        public AgregarEditarNovedadChoferPresenter(
            IChoferRepositorio choferRepositorio,
            IChoferEstadoRepositorio choferEstadoRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _choferRepositorio = choferRepositorio;
            _choferEstadoRepositorio = choferEstadoRepositorio;
        }

        public async Task InicializarAsync(NovedadesChoferesDto novActual)
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<Chofer> choferes = await _choferRepositorio.ObtenerTodosLosChoferes();
                List<ChoferTipoEstado> estados = await _choferEstadoRepositorio.ObtenerEstados();

                var choferesOrdenados = choferes.OrderBy(c => c.Apellido).ToList();

                _view.CargarChoferes(choferesOrdenados);
                _view.CargarEstados(estados);
            });

            if (novActual != null)
            {
                NovedadActual = novActual;
                _view.MostrarDatosNovedad(novActual);
            }
        }

        public async Task GuardarAsync()
        {
            var novedadChofer = new ChoferEstado
            {
                IdEstadoChofer = NovedadActual?.idEstadoChofer ?? 0,
                IdChofer = _view.IdChofer,
                IdEstado = _view.IdEstado,
                FechaInicio = _view.FechaInicio,
                FechaFin = _view.FechaFin,
                Observaciones = _view.Observaciones,
                Disponible = _view.Disponible
            };
            if (await ValidarAsync(novedadChofer))
            {
                await EjecutarConCargaAsync(async () =>
                {
                    if (NovedadActual == null)
                    {
                        await _choferEstadoRepositorio.AltaNovedadAsync(novedadChofer, _sesionService.IdUsuario);
                        _view.MostrarMensaje("Chofer Agregado Correctamente");
                    }
                    else
                    {
                        await _choferEstadoRepositorio.EditarNovedadAsync(novedadChofer, _sesionService.IdUsuario);
                        _view.MostrarMensaje("Chofer Actualizado Correctamente");
                    }
                    _view.Close();
                });
            }
        }

        public void CalcularAusencia()
        {
            if (_view.FechaFin >= _view.FechaInicio)
            {
                int dias = (_view.FechaFin - _view.FechaInicio).Days + 1;
                DateTime reincorporacion = _view.FechaFin.AddDays(1);
                _view.MostrarDiasAusente(dias);
                _view.MostrarFechaReincorporacion(reincorporacion);
            }
            else
            {
                _view.MostrarDiasAusente(0);
                _view.MostrarFechaReincorporacion(_view.FechaInicio);
            }
        }
    }
}