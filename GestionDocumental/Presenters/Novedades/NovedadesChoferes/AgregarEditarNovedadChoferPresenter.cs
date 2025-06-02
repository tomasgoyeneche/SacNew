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
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IUnidadMantenimientoRepositorio _unidadMantenimientoRepositorio;

        public NovedadesChoferesDto? NovedadActual { get; private set; }

        public AgregarEditarNovedadChoferPresenter(
            IChoferRepositorio choferRepositorio,
            IChoferEstadoRepositorio choferEstadoRepositorio,
            INominaRepositorio nominaRepositorio,
            IUnidadMantenimientoRepositorio unidadMantenimientoRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _choferRepositorio = choferRepositorio;
            _choferEstadoRepositorio = choferEstadoRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _unidadMantenimientoRepositorio = unidadMantenimientoRepositorio;
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
                await MostrarMantenimientosUnidadDelChoferAsync(novActual.idChofer);
            }
        }

        public async Task MostrarMantenimientosUnidadDelChoferAsync(int idChofer)
        {
            Nomina? nomina = await _nominaRepositorio.ObtenerNominaActivaPorChoferAsync(idChofer, DateTime.Now);
            if (nomina == null)
            {
                _view.MostrarMantenimientosUnidad(""); // Limpiar el label
                return;
            }

            List<UnidadMantenimientoDto> mantenimientos = await _unidadMantenimientoRepositorio.ObtenerPorUnidadAsync(nomina.IdUnidad);

            if (mantenimientos == null || !mantenimientos.Any())
            {
                _view.MostrarMantenimientosUnidad("Sin mantenimientos asignados");
                return;
            }

            // Armar texto a mostrar
            var texto = string.Join(
                   Environment.NewLine,
                   mantenimientos.Select(m =>
                       $"{m.Descripcion} - fecha inicio: {m.FechaInicio:dd/MM/yyyy} - fecha fin: {m.FechaFin:dd/MM/yyyy}")
               );

            _view.MostrarMantenimientosUnidad(texto);
        }

        public async Task GuardarAsync()
        {
            if (!await ValidarFechasDentroDeMantenimientoAsync())
                return;

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

        private async Task<bool> ValidarFechasDentroDeMantenimientoAsync()
        {
            if (_view.IdEstado != 1 && _view.IdEstado != 2)
                return true; // No requiere validación especial

            // Obtener la unidad del chofer seleccionado
            Nomina? nomina = await _nominaRepositorio.ObtenerNominaActivaPorChoferAsync(_view.IdChofer, DateTime.Now);
            if (nomina == null)
                return true; // No hay unidad, dejar pasar

            List<UnidadMantenimientoDto> mantenimientos = await _unidadMantenimientoRepositorio.ObtenerPorUnidadAsync(nomina.IdUnidad);
            if (mantenimientos == null || !mantenimientos.Any())
                return true; // No hay mantenimientos, dejar pasar

            DateTime fechaInicio = _view.FechaInicio.Date;
            DateTime fechaFin = _view.FechaFin.Date;

            // Buscar si algún mantenimiento cubre el rango solicitado
            bool algunaCoincide = mantenimientos.Any(m =>
                fechaInicio >= m.FechaInicio.Date && fechaFin <= m.FechaFin.Date
            );

            if (!algunaCoincide)
            {
                // La fecha NO está dentro de ningún mantenimiento
                if (string.IsNullOrWhiteSpace(_view.Observaciones))
                {
                    _view.MostrarMensaje("La fecha seleccionada no está dentro de ningún mantenimiento asignado a la unidad. Si desea continuar, debe ingresar una observación explicando el motivo.");
                    return false;
                }
                else
                {
                    _view.MostrarMensaje("Atención: La fecha seleccionada no está dentro de ningún mantenimiento. Se guardará igualmente, ya que dejó una observación.");
                    // Esperar a que el usuario confirme o simplemente continuar (según tu preferencia)
                }
            }
            return true;
        }
    }
}