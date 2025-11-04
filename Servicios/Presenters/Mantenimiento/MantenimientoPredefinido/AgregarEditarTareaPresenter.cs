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
    public class AgregarEditarTareaPresenter : BasePresenter<IAgregarEditarTareaView>
    {
        private readonly ITareaRepositorio _tareaRepositorio;
        private readonly IMantenimientoTareaArticuloRepositorio _tareaArticuloRepositorio;
        private readonly IArticuloRepositorio _articuloRepositorio;
        private readonly IOrdenTrabajoArticuloRepositorio _ordenTrabajoArticuloRepositorio;
        private readonly IOrdenTrabajoTareaRepositorio _ordenTrabajoTareaRepositorio;
        private readonly IOrdenTrabajoMantenimientoRepositorio _ordenTrabajoMantenimientoRepositorio;
        private readonly IArticuloStockRepositorio _articuloStockRepositorio;   
        private readonly IOrdenTrabajoRepositorio _ordenTrabajoRepositorio;




        private Tarea? _tarea;
        private OrdenTrabajoTarea? _tareaOrden;
        private string _tipoVista;

        public AgregarEditarTareaPresenter(
            ITareaRepositorio tareaRepositorio,
            IMantenimientoTareaArticuloRepositorio tareaArticuloRepositorio,
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
                                Descripcion = art.Descripcion,
                                Cantidad = ta.Cantidad,
                                PrecioUnitario = art.PrecioUnitario
                            });
                        }
                    }

                    _view.CargarArticulosAsociados(listaDto);

                    // 🔹 Calcular el total de repuestos
                    decimal totalRepuestos = listaDto.Sum(x => x.PrecioTotal);
                    _view.Repuestos = totalRepuestos;

                    _tarea = await _tareaRepositorio.ObtenerPorIdAsync(idTarea);
                    if (_tarea == null)
                    {
                        _view.MostrarMensaje("No se encontró la tarea especificada.");
                        return;
                    }

                    _view.Nombre = _tarea.Nombre;
                    _view.Descripcion = _tarea.Descripcion;
                    _view.Horas = _tarea.Horas ?? 0;
                    _view.ManoObra = _tarea.ManoObra ?? 0;
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
                                Descripcion = art.Descripcion,
                                Cantidad = ta.Cantidad,
                                PrecioUnitario = ta.PrecioUnitario,
                                Estado = ta.Estado  
                            });
                        }
                    }

                    _view.CargarArticulosAsociados(listaDto);

                    // 🔹 Calcular el total de repuestos
                    decimal totalRepuestos = listaDto.Sum(x => x.PrecioTotal);
                    _view.Repuestos = totalRepuestos; 

                    _tareaOrden = await _ordenTrabajoTareaRepositorio.ObtenerPorIdAsync(idTarea);   
                    if (_tareaOrden == null)
                    {
                        _view.MostrarMensaje("No se encontró la tarea de orden de trabajo especificada.");
                        return;
                    }
                    _view.Nombre = _tareaOrden.Nombre;
                    _view.Descripcion = _tareaOrden.Descripcion;
                    _view.Horas = _tareaOrden.Horas ?? 0;
                    _view.ManoObra = _tareaOrden.ManoObra ?? 0;
                }
                // 🔹 Obtener la tarea
                

                // 🔹 Cargar artículos disponibles y asociados
               


        
            });
        }

        public async Task AgregarArticuloAsync()
        {
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
                    Activo = true
                };
                await _ordenTrabajoArticuloRepositorio.AgregarAsync(entidad);
            }
            await InicializarAsync(_tipoVista, _view.IdTarea);
        }

        public async Task EliminarArticuloAsync(int idArticulo, int? idOrdenTrabajoArticulo = null)
        {
            if(_tipoVista == "MantenimientoPredefinido")
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
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarArticuloForm>(async form =>
                {
                    await form._presenter.InicializarAsync(idArticulo, _tipoVista, _view.IdTarea);
                });
            }, async () => await InicializarAsync(_tipoVista, _view.IdTarea));
        }


        public async Task MoverStockArticulo(int idOrdenTrabajoArticulo)
        {
            OrdenTrabajoArticulo? articulo = await _ordenTrabajoArticuloRepositorio.ObtenerPorIdAsync(idOrdenTrabajoArticulo);
            OrdenTrabajoTarea? tarea = await _ordenTrabajoTareaRepositorio.ObtenerPorIdAsync(articulo.IdOrdenTrabajoTarea);
            OrdenTrabajoMantenimiento? mantenimiento = await _ordenTrabajoMantenimientoRepositorio.ObtenerPorIdAsync(tarea.IdOrdenTrabajoMantenimiento);
            OrdenTrabajo? ordenTrabajo = await _ordenTrabajoRepositorio.ObtenerPorIdAsync(mantenimiento.IdOrdenTrabajo);

            if(ordenTrabajo != null)
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
                    await _ordenTrabajoArticuloRepositorio.ActualizarAsync(articulo);
                }
                else if(ordenTrabajo.IdLugarReparacion == 2)
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

        public async Task GuardarAsync()
        {
            if (_tipoVista == "MantenimientoPredefinido")
            {

                if (_tarea == null)
                {
                    _view.MostrarMensaje("No se pudo guardar: la tarea no está inicializada.");
                    return;
                }

                _tarea.Nombre = _view.Nombre;
                _tarea.Descripcion = _view.Descripcion;
                _tarea.Horas = _view.Horas;
                _tarea.ManoObra = _view.ManoObra;

                await EjecutarConCargaAsync(async () =>
                {
                    await _tareaRepositorio.ActualizarAsync(_tarea);
                    _view.MostrarMensaje("Tarea actualizada correctamente.");
                    _view.Cerrar();
                });
            }
            else
            {
                if (_tareaOrden == null)
                {
                    _view.MostrarMensaje("No se pudo guardar: la tarea no está inicializada.");
                    return;
                }

                _tareaOrden.Nombre = _view.Nombre;
                _tareaOrden.Descripcion = _view.Descripcion;
                _tareaOrden.Horas = _view.Horas;
                _tareaOrden.ManoObra = _view.ManoObra;

                await EjecutarConCargaAsync(async () =>
                {
                    await _ordenTrabajoTareaRepositorio.ActualizarAsync(_tareaOrden);
                    _view.MostrarMensaje("Tarea actualizada correctamente.");
                    _view.Cerrar();
                });
            }
           
        }
    }
}
