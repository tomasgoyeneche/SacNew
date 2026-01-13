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
        private readonly IDisponibilidadRepositorio _disponibleRepositorio;
        private readonly ILocacionRepositorio _locacionRepositorio;

        private readonly IUnidadMantenimientoRepositorio _unidadMantenimientoRepositorio;

        public NovedadesChoferesDto? NovedadActual { get; private set; }

        public AgregarEditarNovedadChoferPresenter(
            IChoferRepositorio choferRepositorio,
            IChoferEstadoRepositorio choferEstadoRepositorio,
            INominaRepositorio nominaRepositorio,
            IDisponibilidadRepositorio disponibleRepositorio,
            ILocacionRepositorio locacionRepositorio,
            IUnidadMantenimientoRepositorio unidadMantenimientoRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _choferRepositorio = choferRepositorio;
            _choferEstadoRepositorio = choferEstadoRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _disponibleRepositorio = disponibleRepositorio;
            _locacionRepositorio = locacionRepositorio;
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
                NovedadActual = null;
                _view.LimpiarFormulario();
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

        public async Task MostrarDisponiblesDelChoferAsync(int idChofer)
        {
            Nomina? nomina = await _nominaRepositorio.ObtenerNominaActivaPorChoferAsync(idChofer, DateTime.Now);
            if (nomina == null)
            {
                _view.MostrarDisponiblesChofer(""); // Limpiar el label
                return;
            }

            List<Disponible> disponibles = await _disponibleRepositorio.ObtenerDisponiblePorNomina(nomina.IdNomina);

            if (disponibles == null || !disponibles.Any())
            {
                _view.MostrarDisponiblesChofer("Sin Disponibles asignados");
                return;
            }

            // Filtrar: solo disponibles con fecha >= hoy
            var disponiblesFiltrados = disponibles
                .Where(d => d.FechaDisponible.Date >= DateTime.Today)
                .ToList();

            if (!disponiblesFiltrados.Any())
            {
                _view.MostrarDisponiblesChofer("Sin Disponibles próximos");
                return;
            }

            // Construir el texto
            var textoBuilder = new List<string>();

            foreach (var disponible in disponiblesFiltrados)
            {
                var locacion = await _locacionRepositorio.ObtenerPorIdAsync(disponible.IdOrigen);
                string descripcionLocacion = locacion?.Nombre ?? "Sin locación";

                textoBuilder.Add(
                    $"Disponible: {disponible.FechaDisponible:dd/MM/yyyy} - " +
                    $"Locación: {descripcionLocacion}"
                );
            }

            string texto = string.Join(Environment.NewLine, textoBuilder);

            _view.MostrarDisponiblesChofer(texto);
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
                Disponible = false,
            };
            if (await ValidarAsync(novedadChofer))
            {
                await EjecutarConCargaAsync(async () =>
                {
                    if (NovedadActual == null)
                    {
                        await _choferEstadoRepositorio.AltaNovedadAsync(novedadChofer, _sesionService.IdUsuario);
                        _view.MostrarMensaje("Novedad de Chofer Agregado Correctamente");
                    }
                    else
                    {
                        await _choferEstadoRepositorio.EditarNovedadAsync(novedadChofer, _sesionService.IdUsuario);
                        _view.MostrarMensaje("Novedad de Chofer Actualizada Correctamente");
                    }

                    List<ChoferTipoEstado> estados = await _choferEstadoRepositorio.ObtenerEstados();
                    var estadoSeleccionado = estados.FirstOrDefault(e => e.IdEstado == novedadChofer.IdEstado);

                    if (estadoSeleccionado.Disponible != true)
                    {
                        await CancelarDisponibilidadesPorAusenciaAsync(
                              _view.IdChofer,
                              _view.FechaInicio,
                              _view.FechaFin
                          );
                    }

                    _view.Close();
                });
            }
        }

        public void CalcularAusencia()
        {
            var inicio = _view.FechaInicio.Date;
            var fin = _view.FechaFin.Date;

            if (fin >= inicio)
            {
                int dias = (fin - inicio).Days + 1; // inclusivo
                DateTime reincorporacion = fin.AddDays(1);
                _view.MostrarDiasAusente(dias);
                _view.MostrarFechaReincorporacion(reincorporacion);
            }
            else
            {
                _view.MostrarDiasAusente(0);
                _view.MostrarFechaReincorporacion(inicio);
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

        private async Task CancelarDisponibilidadesPorAusenciaAsync(
        int idChofer,
        DateTime fechaInicio,
        DateTime fechaFin)
        {
            DateTime fecha = fechaInicio.Date;

            while (fecha <= fechaFin.Date)
            {
                // 1️⃣ Obtener nómina activa del chofer en esa fecha
                Nomina? nomina = await _nominaRepositorio
                    .ObtenerNominaActivaPorChoferAsync(idChofer, fecha);

                if (nomina != null)
                {
                    // 2️⃣ Buscar disponibilidad de ese día
                    Disponible? disponible =
                        await _disponibleRepositorio
                            .ObtenerDisponiblePorNominaYFechaAsync(
                                nomina.IdNomina,
                                fecha);

                    if (disponible != null)
                    {
                        // 3️⃣ Cancelar disponibilidad
                        disponible.IdDisponibleEstado = 10; // Cancelado
                        disponible.Observaciones = "Agrego/Edito Ausencia de Chofer";

                        await _disponibleRepositorio.ActualizarDisponibleAsync(disponible);

                        await _nominaRepositorio.RegistrarNominaAsync(
                            disponible.IdNomina,
                            "Cancela Disponible",
                             $"Agrego/Edito Ausencia de Chofer Fecha: {fechaInicio:dd/MM/yyyy} - Fin: {fechaFin:dd/MM/yyyy}",
                            _sesionService.IdUsuario
                        );
                    }
                }

                fecha = fecha.AddDays(1);
            }
        }
    }
}