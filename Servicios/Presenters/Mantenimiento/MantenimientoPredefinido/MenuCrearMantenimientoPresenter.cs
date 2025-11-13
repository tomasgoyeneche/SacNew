using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views.Mantenimientos;
using Shared.Models;

namespace Servicios.Presenters
{
    public class MenuCrearMantenimientoPresenter : BasePresenter<IMenuCrearMantenimientoView>
    {
        private readonly IMantenimientoRepositorio _mantenimientoRepositorio;
        private readonly ITipoMantenimientoRepositorio _tipoRepositorio;
        private readonly ITareaRepositorio _tareaRepositorio;
        private readonly IOrdenTrabajoMantenimientoRepositorio _ordenTrabajoMantenimientoRepositorio;
        private readonly IOrdenTrabajoTareaRepositorio _ordenTrabajoTareaRepositorio;
        private readonly IOrdenTrabajoArticuloRepositorio _ordenTrabajoArticuloRepositorio;

        private readonly IMantenimientoTareaRepositorio _mantenimientoTareaRepositorio;

        private readonly IMantenimientoTareaArticuloRepositorio _mantenimientoTareaArticuloRepositorio;
        private readonly IArticuloRepositorio _articuloRepositorio;

        private Shared.Models.Mantenimiento? _mantenimiento;
        private Shared.Models.OrdenTrabajoMantenimiento? _Ordenmantenimiento;

        private string? _tipoVista;

        public MenuCrearMantenimientoPresenter(
            IMantenimientoRepositorio mantenimientoRepositorio,
            ITipoMantenimientoRepositorio tipoRepositorio,
            ITareaRepositorio tareaRepositorio,
            IOrdenTrabajoMantenimientoRepositorio ordenTrabajoMantenimientoRepositorio,
            IOrdenTrabajoArticuloRepositorio ordenTrabajoArticuloRepositorio,
            IMantenimientoTareaRepositorio mantenimientoTareaRepositorio,
            IOrdenTrabajoTareaRepositorio ordenTrabajoTareaRepositorio,
            IMantenimientoTareaArticuloRepositorio mantenimientoTareaArticuloRepositorio,

            IArticuloRepositorio articuloRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _mantenimientoRepositorio = mantenimientoRepositorio;
            _tipoRepositorio = tipoRepositorio;
            _tareaRepositorio = tareaRepositorio;
            _ordenTrabajoMantenimientoRepositorio = ordenTrabajoMantenimientoRepositorio;
            _ordenTrabajoTareaRepositorio = ordenTrabajoTareaRepositorio;
            _ordenTrabajoArticuloRepositorio = ordenTrabajoArticuloRepositorio;
            _mantenimientoTareaRepositorio = mantenimientoTareaRepositorio;
            _mantenimientoTareaArticuloRepositorio = mantenimientoTareaArticuloRepositorio;
            _articuloRepositorio = articuloRepositorio;
        }

        public async Task InicializarAsync(string tipoVista, int idMantenimiento)
        {
            _tipoVista = tipoVista;
            await EjecutarConCargaAsync(async () =>
            {
                List<TipoMantenimiento> tipos = await _tipoRepositorio.ObtenerTodosAsync();
                _view.CargarTiposMantenimiento(tipos, tipoVista);
                _view.CargarAplicaA();

                if (_tipoVista == "MantenimientoPredefinido")
                    _view.CargarFrecuencias();
                else
                    _view.OcultarFrecuencias();

                // 🔹 1. Obtener todas las tareas activas

                List<Tarea> todasLasTareas = await _tareaRepositorio.ObtenerTodosAsync();

                // 🔹 2. Si estamos editando un mantenimiento, filtramos las ya agregadas
                if (idMantenimiento != 0)
                {
                    List<int> idsTareasAsociadas;
                    if (_tipoVista == "MantenimientoPredefinido")
                    {
                        List<MantenimientoTarea> tareasAsociadas = await _mantenimientoTareaRepositorio.ObtenerPorMantenimientoAsync(idMantenimiento);
                        idsTareasAsociadas = tareasAsociadas.Select(t => t.IdTarea).ToList();
                    }
                    else
                    {
                        List<OrdenTrabajoTarea> tareasAsociadas = await _ordenTrabajoTareaRepositorio.ObtenerPorMantenimientoAsync(idMantenimiento);
                        idsTareasAsociadas = tareasAsociadas.Select(t => t.IdTarea ?? 0).ToList();
                    }

                    // 🔹 3. Filtrar las tareas no asociadas
                    todasLasTareas = todasLasTareas
                     .Where(t => !idsTareasAsociadas.Contains(t.IdTarea))
                     .OrderBy(t => t.Nombre)
                     .ToList();
                }

                // 🔹 4. Cargar el combo con las tareas filtradas
                _view.CargarTareasPredefinidas(todasLasTareas);

                if (idMantenimiento != 0)
                {
                    if (_tipoVista == "MantenimientoPredefinido")
                    {
                        _mantenimiento = await _mantenimientoRepositorio.ObtenerPorIdAsync(idMantenimiento);
                        if (_mantenimiento != null)
                        {
                            _view.IdMantenimiento = _mantenimiento.IdMantenimiento;
                            _view.Nombre = _mantenimiento.Nombre;
                            _view.IdTipoMantenimiento = _mantenimiento.IdTipoMantenimiento;
                            _view.AplicaA = _mantenimiento.AplicaA;
                            _view.Descripcion = _mantenimiento.Descripcion;
                            _view.KilometrosIntervalo = _mantenimiento.KilometrosIntervalo;
                            _view.DiasIntervalo = _mantenimiento.DiasIntervalo;

                            string frecuenciaInicial;
                            int? valorIntervalo;
                            if (_mantenimiento.DiasIntervalo.HasValue && _mantenimiento.DiasIntervalo.Value > 0)
                            {
                                frecuenciaInicial = "Días";
                                valorIntervalo = _mantenimiento.DiasIntervalo;
                            }
                            else if (_mantenimiento.KilometrosIntervalo.HasValue && _mantenimiento.KilometrosIntervalo.Value > 0)
                            {
                                frecuenciaInicial = "Kilómetros";
                                valorIntervalo = _mantenimiento.KilometrosIntervalo;
                            }
                            else
                            {
                                frecuenciaInicial = "Indefinido";
                                valorIntervalo = null;
                            }

                            _view.SeleccionarFrecuencia(frecuenciaInicial, valorIntervalo);

                            // cargar tareas asociadas
                            var tareasAsociadas = await _mantenimientoTareaRepositorio.ObtenerPorMantenimientoAsync(idMantenimiento);
                            var listaTareas = new List<Tarea>();

                            foreach (var t in tareasAsociadas)
                            {
                                var tarea = await _tareaRepositorio.ObtenerPorIdAsync(t.IdTarea);
                                if (tarea != null) listaTareas.Add(tarea);
                            }

                            _view.CargarTareasAsignadas(listaTareas);
                            await CalcularTotalesAsync(listaTareas, null);
                        }
                    }
                    else
                    {
                        _Ordenmantenimiento = await _ordenTrabajoMantenimientoRepositorio.ObtenerPorIdAsync(idMantenimiento);
                        if (_Ordenmantenimiento != null)
                        {
                            _view.IdMantenimiento = _Ordenmantenimiento.IdOrdenTrabajoMantenimiento;
                            _view.Nombre = _Ordenmantenimiento.Nombre;
                            _view.IdTipoMantenimiento = _Ordenmantenimiento.IdTipoMantenimiento.Value;
                            _view.AplicaA = _Ordenmantenimiento.AplicaA;
                            _view.Descripcion = _Ordenmantenimiento.Descripcion;
                            _view.HorasTotales = _Ordenmantenimiento.Horas ?? 0;
                            _view.ManoObraTotal = _Ordenmantenimiento.ManoObra ?? 0;
                            _view.RepuestosTotales = _Ordenmantenimiento.PrecioRepuestos ?? 0;
                        }

                        // cargar tareas asociadas
                        List<OrdenTrabajoTarea> tareasAsociadas2 = await _ordenTrabajoTareaRepositorio.ObtenerPorMantenimientoAsync(idMantenimiento);
                        _view.CargarTareasAsignadasOrdenes(tareasAsociadas2);
                        await CalcularTotalesAsync(null, tareasAsociadas2);
                    }
                }
            });
        }

        private async Task CalcularTotalesAsync(List<Tarea>? tareas, List<OrdenTrabajoTarea>? tareasOrdenes)
        {
            decimal totalHoras = 0;
            decimal totalManoObra = 0;
            decimal totalRepuestos = 0;
            if (_tipoVista == "MantenimientoPredefinido")
            {
                totalHoras = tareas.Sum(t => t.Horas ?? 0);
                totalManoObra = tareas.Sum(t => t.ManoObra ?? 0);
                totalRepuestos = 0;

                foreach (var tarea in tareas)
                {
                    var articulosAsociados = await _mantenimientoTareaArticuloRepositorio.ObtenerPorTareaAsync(tarea.IdTarea);
                    foreach (var item in articulosAsociados)
                    {
                        var articulo = await _articuloRepositorio.ObtenerPorIdAsync(item.IdArticulo);
                        if (articulo != null)
                        {
                            totalRepuestos += (articulo.PrecioUnitario * item.Cantidad);
                        }
                    }
                }
            }
            else
            {
                totalHoras = tareasOrdenes.Sum(t => t.Horas ?? 0);
                totalManoObra = tareasOrdenes.Sum(t => t.ManoObra ?? 0);
                totalRepuestos = 0;

                foreach (var tarea in tareasOrdenes)
                {
                    List<OrdenTrabajoArticulo> articulosAsociados = await _ordenTrabajoArticuloRepositorio.ObtenerPorTareaAsync(tarea.IdOrdenTrabajoTarea);
                    foreach (var item in articulosAsociados)
                    {
                        totalRepuestos += (item.PrecioUnitario * item.Cantidad);
                    }
                }
            }

            _view.HorasTotales = totalHoras;
            _view.ManoObraTotal = totalManoObra;
            _view.RepuestosTotales = totalRepuestos;
        }

        public async Task GuardarAsync()
        {
            // según el tipo de mantenimiento, actuamos distinto
            if (_tipoVista == "MantenimientoPredefinido")
            {
                var mantenimiento = new Shared.Models.Mantenimiento
                {
                    IdMantenimiento = _mantenimiento?.IdMantenimiento ?? 0,
                    Nombre = _view.Nombre,
                    IdTipoMantenimiento = _view.IdTipoMantenimiento,
                    AplicaA = _view.AplicaA,
                    Descripcion = _view.Descripcion,
                    Activo = true,
                    KilometrosIntervalo = _view.KilometrosIntervalo,
                    DiasIntervalo = _view.DiasIntervalo
                };

                await EjecutarConCargaAsync(async () =>
                {
                    if (_mantenimiento == null)
                    {
                        var id = await _mantenimientoRepositorio.AgregarAsync(mantenimiento);
                        mantenimiento.IdMantenimiento = id;
                        _view.MostrarMensaje("Mantenimiento agregado correctamente.");
                    }
                    else
                    {
                        await _mantenimientoRepositorio.ActualizarAsync(mantenimiento);
                        _view.MostrarMensaje("Mantenimiento actualizado correctamente.");
                    }
                });
            }
            else
            {
                // el comportamiento de proceso se define más adelante
                OrdenTrabajoMantenimiento ordenMantenimiento = new OrdenTrabajoMantenimiento
                {
                    IdOrdenTrabajoMantenimiento = _view.IdMantenimiento,
                    IdOrdenTrabajo = _Ordenmantenimiento.IdOrdenTrabajo,
                    IdMantenimiento = _Ordenmantenimiento.IdMantenimiento ?? null,
                    Nombre = _view.Nombre,
                    IdTipoMantenimiento = _view.IdTipoMantenimiento,
                    AplicaA = _view.AplicaA,
                    Descripcion = _view.Descripcion,
                    Activo = true,
                    ManoObra = _view.ManoObraTotal,
                    Horas = _view.HorasTotales,
                    PrecioRepuestos = _view.RepuestosTotales
                };

                await _ordenTrabajoMantenimientoRepositorio.ActualizarAsync(ordenMantenimiento);
                _view.MostrarMensaje("Mantenimiento actualizado correctamente.");
            }
            _view.Cerrar();
        }

        public async Task AgregarTareaAsync(int idTarea)
        {
            if (_view.IdMantenimiento == 0 && _Ordenmantenimiento == null)
            {
                _view.MostrarMensaje("Debe guardar el mantenimiento antes de agregar tareas.");
                return;
            }

            if (_tipoVista == "MantenimientoPredefinido")
            {
                var mt = new MantenimientoTarea
                {
                    IdMantenimiento = _view.IdMantenimiento,
                    IdTarea = idTarea,
                    Activo = true
                };

                await EjecutarConCargaAsync(async () =>
                {
                    await _mantenimientoTareaRepositorio.AgregarAsync(mt);
                });
            }
            else
            {
                Tarea tarea = await _tareaRepositorio.ObtenerPorIdAsync(idTarea);
                List<MantenimientoTareaArticulo> manTareaArt = await _mantenimientoTareaArticuloRepositorio.ObtenerPorTareaAsync(idTarea);

                List<Articulo> articulos = new List<Articulo>();
                foreach (MantenimientoTareaArticulo item in manTareaArt)
                {
                    Articulo? articulo = await _articuloRepositorio.ObtenerPorIdAsync(item.IdArticulo);
                    articulos.Add(articulo);
                }

                decimal totalArticulos = 0;
                foreach (Articulo articulo in articulos)
                {
                    var cantidad = manTareaArt.FirstOrDefault(x => x.IdArticulo == articulo.IdArticulo)?.Cantidad ?? 0;
                    totalArticulos += articulo.PrecioUnitario * cantidad;
                }

                decimal totalEstimado = tarea.ManoObra.Value + totalArticulos;

                OrdenTrabajoTarea ordenTrabajoTarea = new OrdenTrabajoTarea
                {
                    IdOrdenTrabajoMantenimiento = _Ordenmantenimiento.IdOrdenTrabajoMantenimiento,
                    IdTarea = idTarea,
                    Nombre = tarea.Nombre,
                    Descripcion = tarea.Descripcion,
                    Horas = tarea.Horas,
                    ManoObra = tarea.ManoObra,
                    TotalEstimado = totalEstimado,
                    Activo = true
                };

                int idOrdenTrabajoTarea = await _ordenTrabajoTareaRepositorio.AgregarAsync(ordenTrabajoTarea);

                foreach (Articulo articulo in articulos)
                {
                    var cantidad = manTareaArt.FirstOrDefault(x => x.IdArticulo == articulo.IdArticulo)?.Cantidad ?? 0;

                    OrdenTrabajoArticulo ordenTrabajoTareaArticulo = new OrdenTrabajoArticulo
                    {
                        IdOrdenTrabajoTarea = idOrdenTrabajoTarea,
                        IdArticulo = articulo.IdArticulo,
                        Codigo = articulo.Codigo,
                        Nombre = articulo.Nombre,
                        PrecioUnitario = articulo.PrecioUnitario,
                        Cantidad = cantidad,
                        Estimado = articulo.PrecioUnitario * cantidad,
                        Activo = true,
                        Estado = "Pendiente"
                    };

                    await _ordenTrabajoArticuloRepositorio.AgregarAsync(ordenTrabajoTareaArticulo);
                }
                _view.MostrarMensaje("Tarea Agregada Correctamente");
            }

            await InicializarAsync(_tipoVista, _view.IdMantenimiento);
        }

        public async Task EliminarTareaAsync(int? idTarea = null, int? idOrdenTrabajoTarea = null)
        {
            if (_view.IdMantenimiento == 0) return;

            if (idTarea != null)
            {
                var tareasAsociadas = await _mantenimientoTareaRepositorio.ObtenerPorMantenimientoAsync(_view.IdMantenimiento);
                var registro = tareasAsociadas.FirstOrDefault(t => t.IdTarea == idTarea);
                if (registro != null)
                {
                    await _mantenimientoTareaRepositorio.EliminarAsync(registro.IdMantenimientoTarea);
                }
            }
            else
            {
                OrdenTrabajoTarea? registro = await _ordenTrabajoTareaRepositorio.ObtenerPorIdAsync(idOrdenTrabajoTarea.Value);
                foreach (OrdenTrabajoArticulo item in await _ordenTrabajoArticuloRepositorio.ObtenerPorTareaAsync(registro.IdOrdenTrabajoTarea))
                {
                    await _ordenTrabajoArticuloRepositorio.EliminarAsync(item.IdOrdenTrabajoArticulo);
                    // reestablecer stock si es necesario
                }
            }
            _view.MostrarMensaje("Tarea eliminada correctamente.");

            await InicializarAsync(_tipoVista, _view.IdMantenimiento);
        }

        public async Task<int> CrearTareaAsync(string nombre)
        {
            if (_tipoVista == "MantenimientoPredefinido")
            {
                var tarea = new Tarea
                {
                    Nombre = nombre.Trim(),
                    Descripcion = "",
                    Horas = 0,
                    ManoObra = 0,
                    Activo = true
                };


                int idTarea = await _tareaRepositorio.AgregarAsync(tarea);

                var mt = new MantenimientoTarea
                {
                    IdMantenimiento = _view.IdMantenimiento,
                    IdTarea = idTarea,
                    Activo = true
                };

                await _mantenimientoTareaRepositorio.AgregarAsync(mt);

                return idTarea;

               
            }
            else
            {
                OrdenTrabajoTarea ordenTrabajoTarea = new OrdenTrabajoTarea
                {
                    IdOrdenTrabajoMantenimiento = _Ordenmantenimiento.IdOrdenTrabajoMantenimiento,
                    IdTarea = null,
                    Nombre = nombre.Trim(),
                    Descripcion = "",
                    Horas = 0,
                    ManoObra = 0,
                    TotalEstimado = 0,
                    Activo = true
                };

                return await _ordenTrabajoTareaRepositorio.AgregarAsync(ordenTrabajoTarea);
            }
        }

        public async Task AbrirEdicionTareaAsync(int idTarea)
        {
            await AbrirFormularioAsync<AgregarEditarTareaForm>(async form =>
            {
                await form._presenter.InicializarAsync(_tipoVista, idTarea);
            });

            // 🔹 Refrescar lista de tareas al cerrar
            await InicializarAsync(_tipoVista, _view.IdMantenimiento);
        }
    }
}