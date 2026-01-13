using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views.Mantenimientos;
using Shared.Models;

namespace Servicios.Presenters
{
    public class AgregarEditarTareaPresenter : BasePresenter<IAgregarEditarTareaView>
    {
        private readonly ITareaRepositorio _tareaRepositorio;
        private readonly IMantenimientoTareaArticuloRepositorio _tareaArticuloRepositorio;
        private readonly IArticuloRepositorio _articuloRepositorio;
        private readonly IOrdenTrabajoArticuloRepositorio _ordenTrabajoArticuloRepositorio;
        private readonly IOrdenTrabajoTareaRepositorio _ordenTrabajoTareaRepositorio;
        private readonly IOrdenTrabajoMantenimientoRepositorio _ordenTrabajoMantenimientoRepositorio;
        private readonly IArticuloStockRepositorio _articuloStockRepositorio;
        private readonly IMovimientoStockRepositorio _movimientoStockRepositorio;
        private readonly IMovimientoStockDetalleRepositorio _movimientoStockDetalleRepositorio;
        private readonly IOrdenTrabajoRepositorio _ordenTrabajoRepositorio;

        private Tarea? _tarea;
        private OrdenTrabajoTarea? _tareaOrden;
        private string _tipoVista;

        public AgregarEditarTareaPresenter(
            ITareaRepositorio tareaRepositorio,
            IMantenimientoTareaArticuloRepositorio tareaArticuloRepositorio,
            IMovimientoStockRepositorio movimientoStockRepositorio,
            IMovimientoStockDetalleRepositorio movimientoStockDetalleRepositorio,
            IOrdenTrabajoArticuloRepositorio ordenTrabajoArticuloRepositorio,
            IOrdenTrabajoTareaRepositorio ordenTrabajoTareaRepositorio,
            IArticuloStockRepositorio articuloStockRepositorio,
            IOrdenTrabajoMantenimientoRepositorio ordenTrabajoMantenimientoRepositorio,
            IOrdenTrabajoRepositorio ordenTrabajoRepositorio,
            IArticuloRepositorio articuloRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _tareaRepositorio = tareaRepositorio;
            _tareaArticuloRepositorio = tareaArticuloRepositorio;
            _ordenTrabajoArticuloRepositorio = ordenTrabajoArticuloRepositorio;
            _ordenTrabajoMantenimientoRepositorio = ordenTrabajoMantenimientoRepositorio;
            _movimientoStockRepositorio = movimientoStockRepositorio;
            _movimientoStockDetalleRepositorio = movimientoStockDetalleRepositorio;
            _articuloStockRepositorio = articuloStockRepositorio;
            _ordenTrabajoRepositorio = ordenTrabajoRepositorio;
            _ordenTrabajoTareaRepositorio = ordenTrabajoTareaRepositorio;
            _articuloRepositorio = articuloRepositorio;
        }

        public async Task InicializarAsync(string tipoVista, int idTarea)
        {
            _tipoVista = tipoVista;
            _view.IdTarea = idTarea;

            await EjecutarConCargaAsync(async () =>
            {
                List<Articulo> articulos = await _articuloRepositorio.ObtenerArticulosActivosAsync();
                _view.CargarArticulos(articulos);
                List<TareaArticuloDto> listaDto = new List<TareaArticuloDto>();

                if (_tipoVista == "MantenimientoPredefinido")
                {
                    _view.MostrarMovimientoStock(false);

                    List<MantenimientoTareaArticulo> articulosAsociados = await _tareaArticuloRepositorio.ObtenerPorTareaAsync(idTarea);

                    foreach (var ta in articulosAsociados)
                    {
                        var art = await _articuloRepositorio.ObtenerPorIdAsync(ta.IdArticulo);
                        if (art != null)
                        {
                            listaDto.Add(new TareaArticuloDto
                            {
                                IdArticulo = art.IdArticulo,
                                Codigo = art.Codigo,
                                Descripcion = art.Nombre,
                                Cantidad = ta.Cantidad,
                                PrecioUnitario = art.PrecioUnitario,
                                Dolar = art.Dolar
                            });
                        }
                    }

                    _view.CargarArticulosAsociados(listaDto);

                    // 🔹 Calcular el total de repuestos
                    decimal totalRepuestosPesos = listaDto
                    .Where(x => !x.Dolar)
                    .Sum(x => x.PrecioTotal);

                    decimal totalRepuestosUsd = listaDto
                        .Where(x => x.Dolar)
                        .Sum(x => x.PrecioTotal);

                    _view.Repuestos = totalRepuestosPesos;
                    _view.RepuestosUsd = totalRepuestosUsd;

                    _tarea = await _tareaRepositorio.ObtenerPorIdAsync(idTarea);
                    if (_tarea == null)
                    {
                        _view.MostrarMensaje("No se encontró la tarea especificada.");
                        return;
                    }
                    _view.Codigo = _tarea.Codigo;
                    _view.Nombre = _tarea.Nombre;
                    _view.Descripcion = _tarea.Descripcion;
                    _view.Horas = _tarea.Horas ?? 0;
                    _view.ManoObra = _tarea.ManoObra ?? 0;
                    _view.Dolarizado = _tarea.Dolar;
                }
                else
                {
                    _view.MostrarMovimientoStock(true);

                    List<OrdenTrabajoArticulo> articulosAsociados = await _ordenTrabajoArticuloRepositorio.ObtenerPorTareaAsync(idTarea);

                    foreach (var ta in articulosAsociados)
                    {
                        var art = await _articuloRepositorio.ObtenerPorIdAsync(ta.IdArticulo.Value);
                        if (art != null)
                        {
                            listaDto.Add(new TareaArticuloDto
                            {
                                IdOrdenTrabajoArticulo = ta.IdOrdenTrabajoArticulo,
                                IdArticulo = art.IdArticulo,
                                Codigo = ta.Codigo,
                                Descripcion = ta.Nombre,
                                Cantidad = ta.Cantidad,
                                PrecioUnitario = ta.PrecioUnitario,
                                Estado = ta.Estado,
                                Dolar = ta.Dolar
                            });
                        }
                    }

                    _view.CargarArticulosAsociados(listaDto);

                    // 🔹 Calcular el total de repuestos
                    decimal totalRepuestosPesos = listaDto
                    .Where(x => !x.Dolar)
                    .Sum(x => x.PrecioTotal);

                    decimal totalRepuestosUsd = listaDto
                        .Where(x => x.Dolar)
                        .Sum(x => x.PrecioTotal);

                    _view.Repuestos = totalRepuestosPesos;
                    _view.RepuestosUsd = totalRepuestosUsd;

                    _tareaOrden = await _ordenTrabajoTareaRepositorio.ObtenerPorIdAsync(idTarea);
                    if (_tareaOrden == null)
                    {
                        _view.MostrarMensaje("No se encontró la tarea de orden de trabajo especificada.");
                        return;
                    }
                    _view.Codigo = _tareaOrden.Codigo;
                    _view.Nombre = _tareaOrden.Nombre;
                    _view.Descripcion = _tareaOrden.Descripcion;
                    _view.Horas = _tareaOrden.Horas ?? 0;
                    _view.ManoObra = _tareaOrden.ManoObra ?? 0;
                    _view.Dolarizado = _tareaOrden.Dolar;
                }
                // 🔹 Obtener la tarea

                // 🔹 Cargar artículos disponibles y asociados
            });

            await GuardarAsync(false);
        }

        public async Task AgregarArticuloAsync()
        {
            await GuardarAsync(false);
            if (_view.IdArticuloSeleccionado == 0 || _view.CantidadArticulo <= 0)
            {
                _view.MostrarMensaje("Debe seleccionar un artículo y una cantidad válida.");
                return;
            }
            if (_tipoVista == "MantenimientoPredefinido")
            {
                var existente = await _tareaArticuloRepositorio.ObtenerPorTareaYArticuloAsync(_view.IdTarea, _view.IdArticuloSeleccionado);
                if (existente != null)
                {
                    _view.MostrarMensaje("El artículo ya está agregado a esta tarea.");
                    return;
                }

                MantenimientoTareaArticulo entidad = new MantenimientoTareaArticulo
                {
                    IdTarea = _view.IdTarea,
                    IdArticulo = _view.IdArticuloSeleccionado,
                    Cantidad = _view.CantidadArticulo,
                    Activo = true
                };
                await _tareaArticuloRepositorio.AgregarAsync(entidad);
            }
            else
            {
                var existente = await _ordenTrabajoArticuloRepositorio.ObtenerPorTareaYArticuloAsync(_view.IdTarea, _view.IdArticuloSeleccionado);
                if (existente != null)
                {
                    _view.MostrarMensaje("El artículo ya está agregado a esta tarea.");
                    return;
                }

                Articulo? articulo = await _articuloRepositorio.ObtenerPorIdAsync(_view.IdArticuloSeleccionado);

                OrdenTrabajoArticulo entidad = new OrdenTrabajoArticulo
                {
                    IdOrdenTrabajoTarea = _view.IdTarea,
                    IdArticulo = _view.IdArticuloSeleccionado,
                    Codigo = articulo.Codigo,
                    Nombre = articulo.Descripcion,
                    PrecioUnitario = articulo.PrecioUnitario,
                    Cantidad = _view.CantidadArticulo,
                    Estimado = _view.CantidadArticulo * articulo.PrecioUnitario,
                    Dolar = articulo.Dolar,
                    Activo = true
                };
                await _ordenTrabajoArticuloRepositorio.AgregarAsync(entidad);
            }
            await InicializarAsync(_tipoVista, _view.IdTarea);
        }

        public async Task EliminarArticuloAsync(int idArticulo, int? idOrdenTrabajoArticulo = null)
        {
            await GuardarAsync(false);
            if (_tipoVista == "MantenimientoPredefinido")
            {
                var existentes = await _tareaArticuloRepositorio.ObtenerPorTareaAsync(_view.IdTarea);
                var registro = existentes.FirstOrDefault(x => x.IdArticulo == idArticulo);

                if (registro != null)
                {
                    await _tareaArticuloRepositorio.EliminarAsync(registro.IdMantenimientoTareaArticulo);
                }
            }
            else
            {
                OrdenTrabajoArticulo? articulo = await _ordenTrabajoArticuloRepositorio.ObtenerPorIdAsync(idOrdenTrabajoArticulo.Value);
                if (articulo != null && articulo.Estado != "Confirmado")
                {
                    await _ordenTrabajoArticuloRepositorio.EliminarAsync(articulo.IdOrdenTrabajoArticulo);
                }
                else
                {
                    _view.MostrarMensaje("No se puede eliminar un artículo que ya movio stock.");
                    return;
                }
            }

            await InicializarAsync(_tipoVista, _view.IdTarea);
        }

        public async Task AgregarEditarArticulosAsync(int? idArticulo = null)
        {
            await GuardarAsync(false);
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditArticuloForm>(async form =>
                {
                    await form._presenter.InicializarAsync(idArticulo, _tipoVista, _view.IdTarea);
                });
            }, async () => await InicializarAsync(_tipoVista, _view.IdTarea));
        }

        public async Task MoverStockArticulo(int idOrdenTrabajoArticulo)
        {
            await GuardarAsync(false);
            OrdenTrabajoArticulo? articulo = await _ordenTrabajoArticuloRepositorio.ObtenerPorIdAsync(idOrdenTrabajoArticulo);
            OrdenTrabajoTarea? tarea = await _ordenTrabajoTareaRepositorio.ObtenerPorIdAsync(articulo.IdOrdenTrabajoTarea);
            OrdenTrabajoMantenimiento? mantenimiento = await _ordenTrabajoMantenimientoRepositorio.ObtenerPorIdAsync(tarea.IdOrdenTrabajoMantenimiento);
            OrdenTrabajo? ordenTrabajo = await _ordenTrabajoRepositorio.ObtenerPorIdAsync(mantenimiento.IdOrdenTrabajo);

            int idMovimientoStock = 0;

            if (ordenTrabajo.IdLugarReparacion == 1 || ordenTrabajo.IdLugarReparacion == 2)
            {
                MovimientoStock? movimientoStock = await _movimientoStockRepositorio.ObtenerPorFechaEmisionAsync(ordenTrabajo.FechaEmision);
                if (movimientoStock == null)
                {
                    MovimientoStock movStock = new MovimientoStock
                    {
                        IdTipoMovimiento = 2,
                        FechaEmision = ordenTrabajo.FechaEmision,
                        FechaIngreso = DateTime.Now,
                        Autorizado = true,
                        Observaciones = $"Movimiento por orden de trabajo N° {ordenTrabajo.IdOrdenTrabajo}",
                        Activo = true
                    };

                    int idMovimientoStockNuevo = await _movimientoStockRepositorio.AgregarAsync(movStock);
                    idMovimientoStock = idMovimientoStockNuevo;
                }
                else
                {
                    idMovimientoStock = movimientoStock.IdMovimientoStock;
                }
            }

            if (ordenTrabajo != null)
            {
                if (ordenTrabajo.IdLugarReparacion == 1)
                {
                    ArticuloStock? stock = await _articuloStockRepositorio.ObtenerStockAsync(articulo.IdArticulo.Value, 2);

                    if (stock != null)
                    {
                        stock.CantidadActual = (stock.CantidadActual ?? 0) - articulo.Cantidad;

                        await _articuloStockRepositorio.ActualizarStockAsync(stock);
                    }
                    else
                    {
                        await _articuloStockRepositorio.CrearStockAsync(articulo.IdArticulo.Value, 2, articulo.Cantidad);
                    }

                    articulo.Estado = "Confirmado";

                    MovimientoStockDetalle detalle = new MovimientoStockDetalle
                    {
                        IdMovimientoStock = idMovimientoStock,
                        IdArticulo = articulo.IdArticulo.Value,
                        IdPosta = 2,
                        Cantidad = articulo.Cantidad,
                        PrecioUnitario = articulo.PrecioUnitario,
                        PrecioTotal = articulo.Cantidad * articulo.PrecioUnitario,
                        Dolar = articulo.Dolar,
                        Activo = true
                    };

                    await _movimientoStockDetalleRepositorio.AgregarAsync(detalle);

                    await _ordenTrabajoArticuloRepositorio.ActualizarAsync(articulo);
                }
                else if (ordenTrabajo.IdLugarReparacion == 2)
                {
                    ArticuloStock? stock = await _articuloStockRepositorio.ObtenerStockAsync(articulo.IdArticulo.Value, 3);

                    if (stock != null)
                    {
                        stock.CantidadActual = (stock.CantidadActual ?? 0) - articulo.Cantidad;

                        await _articuloStockRepositorio.ActualizarStockAsync(stock);
                    }
                    else
                    {
                        await _articuloStockRepositorio.CrearStockAsync(articulo.IdArticulo.Value, 3, articulo.Cantidad);
                    }

                    MovimientoStockDetalle detalle = new MovimientoStockDetalle
                    {
                        IdMovimientoStock = idMovimientoStock,
                        IdArticulo = articulo.IdArticulo.Value,
                        IdPosta = 3,
                        Cantidad = articulo.Cantidad,
                        PrecioUnitario = articulo.PrecioUnitario,
                        PrecioTotal = articulo.Cantidad * articulo.PrecioUnitario,
                        Dolar = articulo.Dolar,
                        Activo = true
                    };

                    await _movimientoStockDetalleRepositorio.AgregarAsync(detalle);

                    articulo.Estado = "Confirmado";
                    await _ordenTrabajoArticuloRepositorio.ActualizarAsync(articulo);
                }
                else
                {
                    _view.MostrarMensaje("El lugar de reparación no permite mover stock ya que no fue en el taller");
                }
            }
            else
            {
                _view.MostrarMensaje("No se pudo obtener la orden de trabajo asociada");
                return;
            }

            _view.MostrarMensaje("Stock movido correctamente");
            await InicializarAsync(_tipoVista, _view.IdTarea);
        }

        public async Task GuardarAsync(bool Manual)
        {
            if (_tipoVista == "MantenimientoPredefinido")
            {
                if (_tarea == null)
                {
                    _view.MostrarMensaje("No se pudo guardar: la tarea no está inicializada.");
                    return;
                }
                _tarea.Codigo = _view.Codigo;
                _tarea.Nombre = _view.Nombre;
                _tarea.Descripcion = _view.Descripcion;
                _tarea.Horas = _view.Horas;
                _tarea.ManoObra = _view.ManoObra;
                _tarea.Dolar = _view.Dolarizado;
                await EjecutarConCargaAsync(async () =>
                {
                    await _tareaRepositorio.ActualizarAsync(_tarea);
                    if (Manual == true)
                    {
                        _view.MostrarMensaje("Tarea actualizada correctamente.");
                        _view.Cerrar();
                    }
                });
            }
            else
            {
                if (_tareaOrden == null)
                {
                    _view.MostrarMensaje("No se pudo guardar: la tarea no está inicializada.");
                    return;
                }
                _tareaOrden.Codigo = _view.Codigo;
                _tareaOrden.Nombre = _view.Nombre;
                _tareaOrden.Descripcion = _view.Descripcion;
                _tareaOrden.Horas = _view.Horas;
                _tareaOrden.ManoObra = _view.ManoObra;
                _tareaOrden.Dolar = _view.Dolarizado;
                await EjecutarConCargaAsync(async () =>
                {
                    await _ordenTrabajoTareaRepositorio.ActualizarAsync(_tareaOrden);
                    if (Manual == true)
                    {
                        _view.MostrarMensaje("Tarea actualizada correctamente.");
                        _view.Cerrar();
                    }
                });
            }
        }
    }
}