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
        private readonly IOrdenTrabajoRepositorio _ordenTrabajoRepositorio;
        private readonly IDisponibilidadRepositorio _disponibilidadRepositorio;
        private readonly ILocacionRepositorio _locacionRepositorio;

        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IChoferEstadoRepositorio _choferEstadoRepositorio;
        public UnidadMantenimientoDto? NovedadActual { get; private set; }

        public AgregarEditarNovedadUnidadPresenter(
            IUnidadMantenimientoRepositorio unidadMantenimientoRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IDisponibilidadRepositorio disponibilidadRepositorio,
            ISesionService sesionService,
            ILocacionRepositorio locacionRepositorio,
            INominaRepositorio nominaRepositorio,
            IOrdenTrabajoRepositorio ordenTrabajoRepositorio,
            IChoferEstadoRepositorio choferEstadoRepositorio,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _UnidadMantenimientoRepositorio = unidadMantenimientoRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _nominaRepositorio = nominaRepositorio;
            _locacionRepositorio = locacionRepositorio;
            _disponibilidadRepositorio = disponibilidadRepositorio;
            _ordenTrabajoRepositorio = ordenTrabajoRepositorio;
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
                NovedadActual = null;
                _view.LimpiarFormulario();
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

        public async Task MostrarDisponiblesDeUnidadAsync(int idUnidad)
        {
            Nomina? nomina = await _nominaRepositorio.ObtenerNominaActivaPorUnidadAsync(idUnidad, DateTime.Now);
            if (nomina == null)
            {
                _view.MostrarDisponiblesUnidad(""); // Limpiar el label
                return;
            }

            List<Disponible> disponibles = await _disponibilidadRepositorio.ObtenerDisponiblePorNomina(nomina.IdNomina);

            if (disponibles == null || !disponibles.Any())
            {
                _view.MostrarDisponiblesUnidad("Sin Disponibles asignados");
                return;
            }

            // Filtrar: solo disponibles con fecha >= hoy
            var disponiblesFiltrados = disponibles
                .Where(d => d.FechaDisponible.Date >= DateTime.Today)
                .ToList();

            if (!disponiblesFiltrados.Any())
            {
                _view.MostrarDisponiblesUnidad("Sin Disponibles próximos");
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

            _view.MostrarDisponiblesUnidad(texto);
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
                        int idUMan = await _UnidadMantenimientoRepositorio.AltaNovedadAsync(unidadMantenimiento, _sesionService.IdUsuario);
                        _view.MostrarMensaje("Mantenimiento de Unidad Agregado Correctamente");

                        Nomina? nomina = await _nominaRepositorio.ObtenerNominaActivaPorUnidadAsync(_view.IdUnidad, DateTime.Now);

                        OrdenTrabajo orden = new OrdenTrabajo
                        {
                            FechaEmision = DateTime.Now,
                            FechaInicio = null,
                            FechaFin = null,
                            IdNomina = nomina.IdNomina,
                            OdometroIngreso = null,
                            OdometroSalida = null,
                            HorasEstimadas = null,
                            CostoEstimado = null,
                            Fase = 0, // Asumiendo que 1 es la fase inicial
                            IdLugarReparacion = null,
                            IdUnidadMantenimiento = idUMan,
                            Observaciones = $"Novedad asignada: {idUMan} | Fecha Inicio: {unidadMantenimiento.FechaInicio} - Fecha Fin {unidadMantenimiento.FechaFin} | Descripcion: {unidadMantenimiento.Observaciones}",
                            Activo = true
                        };

                        await _ordenTrabajoRepositorio.AgregarAsync(orden);
                    }
                    else
                    {
                        await _UnidadMantenimientoRepositorio.EditarNovedadAsync(unidadMantenimiento, _sesionService.IdUsuario);
                        _view.MostrarMensaje("Mantenimiento de Unidad Actualizado Correctamente");
                    }

                    List<UnidadMantenimientoEstado> estados = await _UnidadMantenimientoRepositorio.ObtenerEstados();
                    var estadoSeleccionado = estados.FirstOrDefault(e => e.IdMantenimientoEstado == unidadMantenimiento.IdMantenimientoEstado);

                    if (estadoSeleccionado.Disponible != true)
                    {
                        await CancelarDisponibilidadesPorMantenimientoAsync(
                              _view.IdUnidad,
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
                int dias = (_view.FechaFin - _view.FechaInicio).Days + 1;
                DateTime reincorporacion = _view.FechaFin.AddDays(1);
                _view.MostrarDiasAusente(dias);
                _view.MostrarFechaReincorporacion(reincorporacion);
            }
            else
            {
                _view.MostrarDiasAusente(0);
                _view.MostrarFechaReincorporacion(inicio);
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

        private async Task CancelarDisponibilidadesPorMantenimientoAsync(
        int idUnidad,
        DateTime fechaInicio,
        DateTime fechaFin)
        {
            DateTime fecha = fechaInicio.Date;

            while (fecha <= fechaFin.Date)
            {
                // 1️⃣ Obtener nómina activa del chofer en esa fecha
                Nomina? nomina = await _nominaRepositorio
                    .ObtenerNominaActivaPorUnidadAsync(idUnidad, fecha);

                if (nomina != null)
                {
                    // 2️⃣ Buscar disponibilidad de ese día
                    Disponible? disponible =
                        await _disponibilidadRepositorio
                            .ObtenerDisponiblePorNominaYFechaAsync(
                                nomina.IdNomina,
                                fecha);

                    if (disponible != null)
                    {
                        // 3️⃣ Cancelar disponibilidad
                        disponible.IdDisponibleEstado = 10; // Cancelado
                        disponible.Observaciones = "Agrego/Edito Mantenimiento";

                        await _disponibilidadRepositorio.ActualizarDisponibleAsync(disponible);

                        await _nominaRepositorio.RegistrarNominaAsync(
                            disponible.IdNomina,
                            "Cancela Disponible",
                             $"Agrego/Edito Mantenimiento Fecha: {fechaInicio:dd/MM/yyyy} - Fin: {fechaFin:dd/MM/yyyy}",
                            _sesionService.IdUsuario
                        );
                    }
                }

                fecha = fecha.AddDays(1);
            }
        }
    }
}