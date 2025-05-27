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
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IChoferEstadoRepositorio _choferEstadoRepositorio;
        public UnidadMantenimientoDto? NovedadActual { get; private set; }

        public AgregarEditarNovedadUnidadPresenter(
            IUnidadMantenimientoRepositorio unidadMantenimientoRepositorio,
            IUnidadRepositorio unidadRepositorio,
            ISesionService sesionService,
            INominaRepositorio nominaRepositorio,
            IChoferEstadoRepositorio choferEstadoRepositorio,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _UnidadMantenimientoRepositorio = unidadMantenimientoRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _choferEstadoRepositorio = choferEstadoRepositorio;
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
                await MostrarAusenciasDelChoferAsync(novActual.idUnidad);
            }
        }

        public async Task MostrarAusenciasDelChoferAsync(int idUnidad)
        {
            Nomina? nomina = await _nominaRepositorio.ObtenerNominaActivaPorUnidadAsync(idUnidad, DateTime.Now);
            if (nomina == null)
            {
                _view.MostrarAusenciasChofer(""); // Limpiar el label
                return;
            }

            List<NovedadesChoferesDto> ausencias = await _choferEstadoRepositorio.ObtenerPorChoferAsync(nomina.IdChofer);

            if (ausencias == null || !ausencias.Any())
            {
                _view.MostrarAusenciasChofer("Sin francos asignados");
                return;
            }

            // Armar texto a mostrar
            var texto = string.Join(
                   Environment.NewLine,
                   ausencias.Select(m =>
                       $"{m.Descripcion} - fecha inicio: {m.FechaInicio:dd/MM/yyyy} - fecha fin: {m.FechaFin:dd/MM/yyyy}")
               );

            _view.MostrarAusenciasChofer(texto);
        }

        public async Task GuardarAsync()
        {
            if (!await ValidarFechasDentroDeAusenciaAsync())
                return;

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

        private async Task<bool> ValidarFechasDentroDeAusenciaAsync()
        {

            //if (_view.IdEstado != 1 && _view.IdEstado != 2)
            //    return true; // No requiere validación especial

            // Obtener la unidad del chofer seleccionado
            Nomina? nomina = await _nominaRepositorio.ObtenerNominaActivaPorUnidadAsync(_view.IdUnidad, DateTime.Now);
            if (nomina == null)
                return true; // No hay unidad, dejar pasar

            List<NovedadesChoferesDto> novedades = await _choferEstadoRepositorio.ObtenerPorChoferAsync(nomina.IdChofer);
            if (novedades == null || !novedades.Any())
                return true; // No hay mantenimientos, dejar pasar

            DateTime fechaInicio = _view.FechaInicio.Date;
            DateTime fechaFin = _view.FechaFin.Date;

            // Buscar si algún mantenimiento cubre el rango solicitado
            bool algunaCoincide = novedades.Any(m =>
                fechaInicio >= m.FechaInicio.Date && fechaFin <= m.FechaFin.Date
            );

            if (!algunaCoincide)
            {
                // La fecha NO está dentro de ningún mantenimiento
                if (string.IsNullOrWhiteSpace(_view.Observaciones))
                {
                    _view.MostrarMensaje("La fecha seleccionada no está dentro de ningúna ausencia asignada al chofer. Si desea continuar, debe ingresar una observación explicando el motivo.");
                    return false;
                }
                else
                {
                    _view.MostrarMensaje("Atención: La fecha seleccionada no está dentro de ningúna ausencia. Se guardará igualmente, ya que dejó una observación.");
                    // Esperar a que el usuario confirme o simplemente continuar (según tu preferencia)
                }
            }
            return true;
        }
    }
}