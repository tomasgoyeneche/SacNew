using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionDocumental.Views.Novedades.NovedadesUnidades;
using Shared.Models;

namespace GestionDocumental.Presenters.Novedades
{
    public class AgregarEditarNovedadUnidadPresenter : BasePresenter<IAgregarEditarNovedadUnidadView>
    {
        private readonly IUnidadMantenimientoRepositorio _UnidadMantenimientoRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        public UnidadMantenimientoDto? NovedadActual { get; private set; }

        public AgregarEditarNovedadUnidadPresenter(
            IUnidadMantenimientoRepositorio unidadMantenimientoRepositorio,
            IUnidadRepositorio unidadRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _UnidadMantenimientoRepositorio = unidadMantenimientoRepositorio;
            _unidadRepositorio = unidadRepositorio;
        }

        public async Task InicializarAsync(UnidadMantenimientoDto novActual)
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<UnidadDto> unidades = await _unidadRepositorio.ObtenerUnidadesDtoAsync();
                List<UnidadMantenimientoEstado> estados = await _UnidadMantenimientoRepositorio.ObtenerEstados();

                var unidadesOrdenadas = unidades.OrderBy(c => c.PatenteCompleta).ToList();

                _view.CargarUnidades(unidadesOrdenadas);
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
            var unidadMantenimiento = new UnidadMantenimiento
            {
                IdUnidadMantenimiento = NovedadActual?.idUnidadMantenimiento ?? 0,
                IdUnidad = _view.IdUnidad,
                IdMantenimientoEstado = _view.IdMantenimientoEstado,
                FechaInicio = _view.FechaInicio,
                FechaFin = _view.FechaFin,
                Observaciones = _view.Observaciones,
                Odometro = _view.Odometro
            };
            if (await ValidarAsync(unidadMantenimiento))
            {
                await EjecutarConCargaAsync(async () =>
                {
                    if (NovedadActual == null)
                    {
                        await _UnidadMantenimientoRepositorio.AltaNovedadAsync(unidadMantenimiento, _sesionService.IdUsuario);
                        _view.MostrarMensaje("Mantenimiento de Unidad Agregado Correctamente");
                    }
                    else
                    {
                        await _UnidadMantenimientoRepositorio.EditarNovedadAsync(unidadMantenimiento, _sesionService.IdUsuario);
                        _view.MostrarMensaje("Mantenimiento de Unidad Actualizado Correctamente");
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