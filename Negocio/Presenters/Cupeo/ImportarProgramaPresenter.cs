using Core.Base;
using Core.Repositories;
using Core.Services;
using DevExpress.XtraEditors;
using GestionFlota.Views;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class ImportarProgramaPresenter : BasePresenter<IImportarProgramaView>
    {
        private readonly ILocacionRepositorio _locacionRepositorio;
        private readonly IProductoRepositorio _productoRepositorio;
        private readonly IChoferRepositorio _choferRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;
        private readonly IPedidoRepositorio _pedidoRepositorio; // El que guarda los pedidos

        public ImportarProgramaPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            ILocacionRepositorio locacionRepositorio,
            IProductoRepositorio productoRepositorio,
            IChoferRepositorio choferRepositorio,
            IUnidadRepositorio unidadRepositorio,
            IPedidoRepositorio pedidoRepositorio)
            : base(sesionService, navigationService)
        {
            _locacionRepositorio = locacionRepositorio;
            _productoRepositorio = productoRepositorio;
            _choferRepositorio = choferRepositorio;
            _unidadRepositorio = unidadRepositorio;
            _pedidoRepositorio = pedidoRepositorio;
        }

        public async Task RevisarAsync()
        {
            var pedidos = _view.ObtenerPedidosImportados().ToList();
            if (pedidos == null || pedidos.Count == 0)
            {
                _view.HabilitarGuardar(false);
                return;
            }
            var errores = new List<ErrorImportacionDto>();

            // Cache para performance
            var productos = await _productoRepositorio.ObtenerTodosAsync();
            var sinonimosProd = await _productoRepositorio.ObtenerTodosSinonimosAsync();
            var locaciones = await _locacionRepositorio.ObtenerTodasAsync();
            var sinonimosLoc = await _locacionRepositorio.ObtenerTodosSinonimosAsync();

            for (int i = 0; i < pedidos.Count; i++)
            {
                var p = pedidos[i];
                int reg = i + 1;

                // Producto
                var prod = productos.FirstOrDefault(x => string.Equals(x.Nombre, p.Producto, StringComparison.OrdinalIgnoreCase));
                if (prod == null)
                {
                    var sinonimoProd = sinonimosProd.FirstOrDefault(x => string.Equals(x.Sinonimo, p.Producto, StringComparison.OrdinalIgnoreCase));
                    if (sinonimoProd != null)
                        prod = productos.FirstOrDefault(x => x.IdProducto == sinonimoProd.IdProducto);
                }
                if (string.IsNullOrWhiteSpace(p.Producto) || prod == null)
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "Producto", Detalle = p.Producto });

                // Locación (cliente)
                var loc = locaciones.FirstOrDefault(x => string.Equals(x.Nombre, p.NombreCliente, StringComparison.OrdinalIgnoreCase));
                if (loc == null)
                {
                    var sinonimoLoc = sinonimosLoc.FirstOrDefault(x => string.Equals(x.Sinonimo, p.NombreCliente, StringComparison.OrdinalIgnoreCase));
                    if (sinonimoLoc != null)
                        loc = locaciones.FirstOrDefault(x => x.IdLocacion == sinonimoLoc.IdLocacion);
                }
                if (string.IsNullOrWhiteSpace(p.NombreCliente) || loc == null)
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "NombreCliente", Detalle = p.NombreCliente });

                // FechaEntrega
                if (p.FechaEntrega == null)
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "FechaEntrega", Detalle = "" });
                else if (p.FechaEntrega.Value < DateTime.Today)
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "FechaEntrega", Detalle = p.FechaEntrega.Value.ToString("dd/MM/yyyy") });

                // FechaCarga
                if (p.FechaCarga == null)
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "FechaCarga", Detalle = "" });
                else if (p.FechaCarga.Value < DateTime.Today)
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "FechaCarga", Detalle = p.FechaCarga.Value.ToString("dd/MM/yyyy") });

                // Cantidad
                if (p.Cantidad == null)
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "Cantidad", Detalle = "" });

                // Transporte y Tractor (validación de existencia puede ser posterior)
                if (string.IsNullOrWhiteSpace(p.Transporte))
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "Transporte", Detalle = p.Transporte });

                if (string.IsNullOrWhiteSpace(p.CodTransporte))
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "CodTransporte", Detalle = p.CodTransporte });

                if (string.IsNullOrWhiteSpace(p.Dni))
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "Dni", Detalle = p.Dni });

                if (string.IsNullOrWhiteSpace(p.Chofer))
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "Chofer", Detalle = p.Chofer });

                if (string.IsNullOrWhiteSpace(p.Tractor))
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "Tractor", Detalle = p.Tractor });

                if (string.IsNullOrWhiteSpace(p.Semi))
                    errores.Add(new ErrorImportacionDto { Registro = reg, Columna = "Semi", Detalle = p.Semi });
            }

            _view.MostrarErrores(errores);
            _view.HabilitarGuardar(!errores.Any());
            if (errores.Any())
                _view.MostrarMensaje("Se encontraron errores. Corrija los datos antes de continuar.");
            else
                _view.MostrarMensaje("¡No se encontraron errores! Puede continuar con la importación.");
        }

        public async Task GuardarAsync()
        {
            var pedidosImportados = _view.ObtenerPedidosImportados().ToList();
            var pedidosParaGuardar = new List<Pedido>();

            var productos = await _productoRepositorio.ObtenerTodosAsync();
            var sinonimosProd = await _productoRepositorio.ObtenerTodosSinonimosAsync();
            var locaciones = await _locacionRepositorio.ObtenerTodasAsync();
            var sinonimosLoc = await _locacionRepositorio.ObtenerTodosSinonimosAsync();

            foreach (var imp in pedidosImportados)
            {
                // Producto
                var prod = productos.FirstOrDefault(x => string.Equals(x.Nombre, imp.Producto, StringComparison.OrdinalIgnoreCase));
                if (prod == null)
                {
                    var sinonimoProd = sinonimosProd.FirstOrDefault(x => string.Equals(x.Sinonimo, imp.Producto, StringComparison.OrdinalIgnoreCase));
                    if (sinonimoProd != null)
                        prod = productos.FirstOrDefault(x => x.IdProducto == sinonimoProd.IdProducto);
                }
                if (prod == null) continue;

                // Locacion
                var loc = locaciones.FirstOrDefault(x => string.Equals(x.Nombre, imp.NombreCliente, StringComparison.OrdinalIgnoreCase));
                if (loc == null)
                {
                    var sinonimoLoc = sinonimosLoc.FirstOrDefault(x => string.Equals(x.Sinonimo, imp.NombreCliente, StringComparison.OrdinalIgnoreCase));
                    if (sinonimoLoc != null)
                        loc = locaciones.FirstOrDefault(x => x.IdLocacion == sinonimoLoc.IdLocacion);
                }
                if (loc == null) continue;

                // Chofer
                int? idChofer = await _choferRepositorio.ObtenerIdPorDocumentoAsync(imp.Dni);
                if (!idChofer.HasValue) continue;

                // Unidad
                int? idTractor = await _unidadRepositorio.ObtenerIdTractorPorPatenteAsync(imp.Tractor);
                if (!idTractor.HasValue) continue;
                int? idUnidad = await _unidadRepositorio.ObtenerIdUnidadPorTractorAsync(idTractor.Value);
                if (!idUnidad.HasValue) continue;

                pedidosParaGuardar.Add(new Pedido
                {
                    IdProducto = prod.IdProducto,
                    AlbaranDespacho = int.TryParse(imp.AlbaranDespacho, out int albaran) ? albaran : 0,
                    PedidoOr = int.TryParse(imp.PedidoOR, out int pedidoOr) ? pedidoOr : 0,
                    IdLocacion = loc.IdLocacion,
                    FechaEntrega = imp.FechaEntrega ?? DateTime.Now,
                    FechaCarga = imp.FechaCarga ?? DateTime.Now,
                    Cantidad = imp.Cantidad ?? 0,
                    IdChofer = idChofer.Value,
                    IdUnidad = idUnidad.Value,
                    Observaciones = imp.Observaciones,
                    IdUsuario = _sesionService.IdUsuario,
                    Fecha = DateTime.Now,
                    Activo = true
                });
            }

            if (pedidosParaGuardar.Any())
            {
                await _pedidoRepositorio.InsertarPedidosAsync(pedidosParaGuardar);
                _view.MostrarMensaje($"Se importaron {pedidosParaGuardar.Count} pedidos correctamente.");
            }
            else
            {
                _view.MostrarMensaje("No se pudo importar ningún pedido. Revise los datos.");
            }
        }

        public async Task GestionarErrorDeSinonimoAsync(ErrorImportacionDto error)
        {
            if (error.Columna == "Producto" && !string.IsNullOrWhiteSpace(error.Detalle))
            {
                // Mostrar selector de productos
                var productos = await _productoRepositorio.ObtenerTodosAsync();
                var selector = new SelectorProductoControl(productos);
                if (XtraDialog.Show(selector, "Seleccionar Producto", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    var seleccionado = selector.ProductoSeleccionado;
                    if (seleccionado != null)
                    {
                        await _productoRepositorio.AgregarSinonimoAsync(new ProductoSinonimo
                        {
                            IdProducto = seleccionado.IdProducto,
                            Sinonimo = error.Detalle.Trim(),
                            IdUsuario = _sesionService.IdUsuario
                        });
                        _view.MostrarMensaje($"Sinónimo '{error.Detalle}' agregado al producto '{seleccionado.Nombre}'. Vuelva a revisar para validar el dato.");
                    }
                }
            }
            else if (error.Columna == "NombreCliente" && !string.IsNullOrWhiteSpace(error.Detalle))
            {
                var locaciones = await _locacionRepositorio.ObtenerTodasAsync();
                var selector = new SelectorLocacionControl(locaciones);
                if (XtraDialog.Show(selector, "Seleccionar Locación", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    var seleccionado = selector.LocacionSeleccionada;
                    if (seleccionado != null)
                    {
                        await _locacionRepositorio.AgregarSinonimoAsync(new LocacionSinonimo
                        {
                            IdLocacion = seleccionado.IdLocacion,
                            Sinonimo = error.Detalle.Trim()
                        });
                        _view.MostrarMensaje($"Sinónimo '{error.Detalle}' agregado a la locación '{seleccionado.Nombre}'. Vuelva a revisar para validar el dato.");
                    }
                }
            }
        }
    }
}