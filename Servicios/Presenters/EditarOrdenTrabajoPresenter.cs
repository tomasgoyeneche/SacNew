using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views.Mantenimiento;
using Servicios.Views.Mantenimientos;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters
{
    public class EditarOrdenTrabajoPresenter : BasePresenter<IEditarOrdenTrabajoView>
    {
        private readonly IOrdenTrabajoRepositorio _ordenRepositorio;
        private readonly IOrdenTrabajoMantenimientoRepositorio _ordenTrabajoMantenimientoRepositorio;
        private readonly IMantenimientoRepositorio _mantenimientoRepositorio;
        private readonly IMantenimientoTareaRepositorio _mantenimientoTareaRepositorio;
        private readonly IMantenimientoTareaArticuloRepositorio _mantenimientoTareaArticuloRepositorio;
        private readonly IOrdenTrabajoTareaRepositorio _ordenTrabajoTareaRepositorio;
        private readonly IOrdenTrabajoArticuloRepositorio _ordenTrabajoArticuloRepositorio;
        private readonly ITareaRepositorio _tareaRepositorio;
        private readonly IArticuloRepositorio _articuloRepositorio;
        private readonly ILugarReparacionRepositorio _lugarRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly INominaRepositorio _nominaRepositorio;
        private readonly IOrdenTrabajoComprobanteRepositorio _ordenTrabajoComprobanteRepositorio;

        private OrdenTrabajo? _ordenActual;

        public EditarOrdenTrabajoPresenter(
            IOrdenTrabajoRepositorio ordenRepositorio,
            IOrdenTrabajoMantenimientoRepositorio ordenTrabajoMantenimientoRepositorio,
            IMantenimientoRepositorio mantenimientoRepositorio,
            IMantenimientoTareaRepositorio mantenimientoTareaRepositorio,
            IOrdenTrabajoTareaRepositorio ordenTrabajoTareaRepositorio,
            IOrdenTrabajoArticuloRepositorio ordenTrabajoArticuloRepositorio,
            ITareaRepositorio tareaRepositorio,
            IMantenimientoTareaArticuloRepositorio mantenimientoTareaArticuloRepositorio,
            IArticuloRepositorio articuloRepositorio,
            ILugarReparacionRepositorio lugarRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IOrdenTrabajoComprobanteRepositorio ordenTrabajoComprobanteRepositorio,
            INominaRepositorio nominaRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _ordenRepositorio = ordenRepositorio;
            _ordenTrabajoMantenimientoRepositorio = ordenTrabajoMantenimientoRepositorio;
            _mantenimientoRepositorio = mantenimientoRepositorio;
            _mantenimientoTareaRepositorio = mantenimientoTareaRepositorio;
            _mantenimientoTareaArticuloRepositorio = mantenimientoTareaArticuloRepositorio;
            _articuloRepositorio = articuloRepositorio;
            _tareaRepositorio = tareaRepositorio;
            _ordenTrabajoTareaRepositorio = ordenTrabajoTareaRepositorio;
            _ordenTrabajoArticuloRepositorio = ordenTrabajoArticuloRepositorio;
            _lugarRepositorio = lugarRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _ordenTrabajoComprobanteRepositorio = ordenTrabajoComprobanteRepositorio;
            _nominaRepositorio = nominaRepositorio;
        }


        public async Task InicializarAsync(int idOrdenTrabajo)
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<UnidadDto> unidades = await _unidadRepositorio.ObtenerUnidadesDtoAsync();
                _view.CargarUnidades(unidades);

                List<LugarReparacion> lugares = await _lugarRepositorio.ObtenerTodosAsync();
                _view.CargarLugares(lugares);

                List<OrdenTrabajoComprobante> comprobantes = await _ordenTrabajoComprobanteRepositorio.ObtenerPorMovimientoAsync(idOrdenTrabajo);
                _view.CargarComprobantes(comprobantes);

                var mantenimientos = await _mantenimientoRepositorio.ObtenerTodosAsync();
                _view.CargarMantenimientosPredefinidos(mantenimientos);

                List<OrdenTrabajoMantenimiento> mantenimientosOrden = await _ordenTrabajoMantenimientoRepositorio.ObtenerPorOrdenTrabajoAsync(idOrdenTrabajo);
                _view.CargarMantenimientosOrden(mantenimientosOrden);

                _ordenActual = await _ordenRepositorio.ObtenerPorIdAsync(idOrdenTrabajo);
                if (_ordenActual == null)
                    throw new Exception("No se encontró la orden de trabajo.");

                _view.IdOrdenTrabajo = _ordenActual.IdOrdenTrabajo;
                _view.FechaEmision = _ordenActual.FechaEmision;
                _view.IdLugarReparacion = _ordenActual.IdLugarReparacion;
                _view.Observaciones = _ordenActual.Observaciones ?? "";
                _view.FechaIngreso = _ordenActual.FechaInicio;
                _view.FechaFin = _ordenActual.FechaFin;
                _view.OdometroIngreso = _ordenActual.OdometroIngreso;
                _view.OdometroSalida = _ordenActual.OdometroSalida;

                // 🔹 Buscar unidad en base a la nómina actual
                if (_ordenActual.IdNomina != null)
                {
                    Nomina? nomina = await _nominaRepositorio.ObtenerPorIdAsync(_ordenActual.IdNomina.Value);
                    _view.IdNomina = nomina?.IdNomina;
                    _view.IdUnidad = nomina?.IdUnidad;
                }

                // 🔹 Actualizar estado general de la interfaz
                _view.ActualizarEstadoUI(_ordenActual.Fase);
            });
        }

        public async Task AutorizarAsync(int idUnidad)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var nomina = await _nominaRepositorio.ObtenerNominaActivaPorUnidadAsync(idUnidad, DateTime.Now);
                if (nomina == null)
                {
                    _view.MostrarMensaje("No se encontró una nómina activa para la unidad seleccionada.");
                    return;
                }

                _ordenActual!.IdNomina = nomina.IdNomina;
                _ordenActual.Fase = 1; // Autorizada
                await _ordenRepositorio.ActualizarAsync(_ordenActual);

                _view.IdNomina = nomina.IdNomina;
                _view.MostrarMensaje("Orden de trabajo autorizada correctamente.");

                _view.ActualizarEstadoUI(_ordenActual.Fase);
            });
        }


        public async Task AgregarMantenimientoAsync(int idMantenimiento)
        {
            if (_ordenActual == null)
                throw new Exception("No se encontró la orden de trabajo actual.");

            var mantenimiento = await _mantenimientoRepositorio.ObtenerPorIdAsync(idMantenimiento);
            if (mantenimiento == null)
            {
                _view.MostrarMensaje("No se encontró el mantenimiento seleccionado.");
                return;
            }

            // Calcular totales
            decimal totalHoras = 0, totalManoObra = 0, totalRepuestos = 0;

            List<MantenimientoTarea> tareasMantenimiento = await _mantenimientoTareaRepositorio.ObtenerPorMantenimientoAsync(idMantenimiento);
            List<Tarea> listaTareas = new List<Tarea>();

            foreach (var tareaMan in tareasMantenimiento)
            {
                Tarea? tarea = await _tareaRepositorio.ObtenerPorIdAsync(tareaMan.IdTarea);
                if (tarea == null) continue;

                listaTareas.Add(tarea);
                totalHoras += tarea.Horas ?? 0;
                totalManoObra += tarea.ManoObra ?? 0;

                var articulos = await _mantenimientoTareaArticuloRepositorio.ObtenerPorTareaAsync(tareaMan.IdTarea);
                foreach (var art in articulos)
                {
                    var articulo = await _articuloRepositorio.ObtenerPorIdAsync(art.IdArticulo);
                    if (articulo != null)
                        totalRepuestos += articulo.PrecioUnitario * art.Cantidad;
                }
            }

            var nuevo = new OrdenTrabajoMantenimiento
            {
                IdOrdenTrabajo = _ordenActual.IdOrdenTrabajo,
                IdMantenimiento = mantenimiento.IdMantenimiento,
                Nombre = mantenimiento.Nombre,
                IdTipoMantenimiento = mantenimiento.IdTipoMantenimiento,
                AplicaA = mantenimiento.AplicaA,
                Descripcion = mantenimiento.Descripcion,
                ManoObra = totalManoObra,
                Horas = totalHoras,
                PrecioRepuestos = totalRepuestos,
                Activo = true
            };

            int idOrdenTrabajoMantenimiento = await _ordenTrabajoMantenimientoRepositorio.AgregarAsync(nuevo);
            // =========================================
            // 🔸 3. Crear OrdenTrabajoTarea + Artículos
            // =========================================
            foreach (var tarea in listaTareas)
            {
                decimal totalRepuestosTarea = 0;
                List<MantenimientoTareaArticulo> articulosTarea = await _mantenimientoTareaArticuloRepositorio.ObtenerPorTareaAsync(tarea.IdTarea);

                // Calculamos total de repuestos de esta tarea
                foreach (MantenimientoTareaArticulo art in articulosTarea)
                {
                    Articulo? articulo = await _articuloRepositorio.ObtenerPorIdAsync(art.IdArticulo);
                    if (articulo != null)
                        totalRepuestosTarea += articulo.PrecioUnitario * art.Cantidad;
                }

                decimal totalEstimado = (tarea.ManoObra ?? 0) + totalRepuestosTarea;

                OrdenTrabajoTarea nuevaTarea = new OrdenTrabajoTarea
                {
                    IdOrdenTrabajoMantenimiento = idOrdenTrabajoMantenimiento,
                    IdTarea = tarea.IdTarea,
                    Nombre = tarea.Nombre,
                    Descripcion = tarea.Descripcion,
                    ManoObra = tarea.ManoObra,
                    Horas = tarea.Horas,
                    TotalEstimado = totalEstimado,
                    Activo = true
                };

                int idOrdenTrabajoTarea = await _ordenTrabajoTareaRepositorio.AgregarAsync(nuevaTarea);

                // 🔹 Ahora cargamos los artículos de esta tarea
                foreach (MantenimientoTareaArticulo art in articulosTarea)
                {
                    Articulo? articulo = await _articuloRepositorio.ObtenerPorIdAsync(art.IdArticulo);
                    if (articulo == null) continue;

                    OrdenTrabajoArticulo nuevoArticulo = new OrdenTrabajoArticulo
                    {
                        IdOrdenTrabajoTarea = idOrdenTrabajoTarea,
                        IdArticulo = articulo.IdArticulo,
                        Codigo = articulo.Codigo,
                        Nombre = articulo.Nombre,
                        PrecioUnitario = articulo.PrecioUnitario,
                        Cantidad = art.Cantidad,
                        Estimado = articulo.PrecioUnitario * art.Cantidad,
                        Activo = true,
                        Estado = "Pendiente"
                    };

                    await _ordenTrabajoArticuloRepositorio.AgregarAsync(nuevoArticulo);
                }
            }

            // =========================================
            // 🔸 4. Refrescar grilla
            // =========================================
            List<OrdenTrabajoMantenimiento> lista = await _ordenTrabajoMantenimientoRepositorio.ObtenerPorOrdenTrabajoAsync(_ordenActual.IdOrdenTrabajo);
            _view.CargarMantenimientosOrden(lista);

            _view.MostrarMensaje("Mantenimiento agregado correctamente a la orden.");
        }

        public async Task ConfirmarIngresoAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                decimal? odometroIngreso = await _nominaRepositorio.ObtenerOdometerPorNomina(_ordenActual.IdNomina!.Value);
                _ordenActual.FechaInicio = DateTime.Now;
                _ordenActual.IdLugarReparacion = _view.IdLugarReparacion;
                _ordenActual.OdometroIngreso = odometroIngreso;
                _ordenActual.Fase = 2; // En Taller

                await _ordenRepositorio.ActualizarAsync(_ordenActual);

                _view.FechaIngreso = _ordenActual.FechaInicio;
                _view.OdometroIngreso = odometroIngreso;
                _view.MostrarMensaje("Ingreso confirmado correctamente.");

                _view.ActualizarEstadoUI(_ordenActual.Fase);
            });
        }

        public async Task ConfirmarSalidaAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                decimal? odometroSalida = await _nominaRepositorio.ObtenerOdometerPorNomina(_ordenActual.IdNomina!.Value);
                _ordenActual.FechaFin = DateTime.Now;
                _ordenActual.OdometroSalida = odometroSalida;
                _ordenActual.Fase = 3; // Finalizada

                await _ordenRepositorio.ActualizarAsync(_ordenActual);

                _view.FechaFin = _ordenActual.FechaFin;
                _view.OdometroSalida = odometroSalida;
                _view.MostrarMensaje("Orden de trabajo finalizada correctamente.");

                _view.ActualizarEstadoUI(_ordenActual.Fase);
            });
        }

        public async Task GuardarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
              
                _ordenActual.FechaEmision = _view.FechaEmision;
                _ordenActual.FechaInicio = _view.FechaIngreso;
                _ordenActual.FechaFin = _view.FechaFin;
                _ordenActual.OdometroSalida = _view.OdometroSalida;
                _ordenActual.OdometroIngreso = _view.OdometroIngreso;
                _ordenActual.Observaciones = _view.Observaciones;   
                _ordenActual.HorasEstimadas = _view.Horas;
                _ordenActual.CostoEstimado = _view.Costo;   

                await _ordenRepositorio.ActualizarAsync(_ordenActual);
                _view.MostrarMensaje("Orden de trabajo Actualizada correctamente.");

                _view.Cerrar();
            });
        }


        public async Task AgregarComprobanteAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarComprobanteForm>(async form =>
                {
                    await form._presenter.InicializarAsync(_ordenActual.IdOrdenTrabajo, "OrdenTrabajo");
                });
            }, async () => await InicializarAsync(_ordenActual.IdOrdenTrabajo));
        }

        public async Task EditarComprobanteAsync(int idOrdenTrabajoComprobante)
        {
            OrdenTrabajoComprobante? detalle = await _ordenTrabajoComprobanteRepositorio.ObtenerPorIdAsync(idOrdenTrabajoComprobante);
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarComprobanteForm>(async form =>
                {
                    await form._presenter.InicializarAsync(_ordenActual.IdOrdenTrabajo, "OrdenTrabajo", null, detalle);
                });
            }, async () => await InicializarAsync(_ordenActual.IdOrdenTrabajo));
        }
    }
}
