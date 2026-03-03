using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views;
using Servicios.Views.Mantenimiento;
using Servicios.Views.RequerimientosDeCompra;
using Shared.Models;
using Shared.Models.RequerimientoCompra;

namespace Servicios.Presenters
{
    public class AgregarEditarMovimientoStockPresenter : BasePresenter<IAgregarEditarMovimientoStockView>
    {
        private readonly IMovimientoStockRepositorio _movimientoRepositorio;
        private readonly IMovimientoStockDetalleRepositorio _detalleRepositorio;
        private readonly IMovimientoComprobanteRepositorio _comprobanteRepositorio;
        private readonly IArticuloRepositorio _articuloRepositorio;
        private readonly IArticuloStockRepositorio _articuloStockRepositorio;
        private readonly IPostaRepositorio _postaRepositorio;

        private readonly IArticuloProveedorRepositorio _articuloProveedorRepositorio;

        private MovimientoStock? _movimiento;

        public AgregarEditarMovimientoStockPresenter(
        IMovimientoStockRepositorio movimientoRepositorio,
        IMovimientoStockDetalleRepositorio detalleRepositorio,
        IMovimientoComprobanteRepositorio comprobanteRepositorio,
        IArticuloRepositorio articuloRepositorio,
        IPostaRepositorio postaRepositorio,
        IArticuloStockRepositorio articuloStockRepositorio, // 🔹 agregado
        IArticuloProveedorRepositorio articuloProveedorRepositorio,
        ISesionService sesionService,
        INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _movimientoRepositorio = movimientoRepositorio;
            _detalleRepositorio = detalleRepositorio;
            _comprobanteRepositorio = comprobanteRepositorio;
            _articuloRepositorio = articuloRepositorio;
            _articuloProveedorRepositorio = articuloProveedorRepositorio;
            _articuloStockRepositorio = articuloStockRepositorio; // 🔹 agregado
            _postaRepositorio = postaRepositorio;
        }

        public async Task InicializarAsync(int idMovimiento)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var tipos = await _movimientoRepositorio.ObtenerTipoMovimientosAsync();
                _view.CargarTiposMovimiento(tipos);

                _movimiento = await _movimientoRepositorio.ObtenerPorIdAsync(idMovimiento);
                if (_movimiento != null)
                {
                    _view.IdTipoMovimiento = _movimiento.IdTipoMovimiento;
                    _view.Autorizado = _movimiento.Autorizado;
                    _view.FechaEmision = _movimiento.FechaEmision;
                    _view.FechaIngreso = _movimiento.FechaIngreso;
                    _view.Observaciones = _movimiento.Observaciones ?? "";

                    List<MovimientoStockDetalle> detalles = await _detalleRepositorio.ObtenerPorMovimientoAsync(_movimiento.IdMovimientoStock);

                    var detallesDto = new List<MovimientoStockDetalleDto>();
                    foreach (var det in detalles)
                    {
                        var articulo = await _articuloRepositorio.ObtenerPorIdAsync(det.IdArticulo);

                        detallesDto.Add(new MovimientoStockDetalleDto
                        {
                            IdMovimientoDetalle = det.IdMovimientoDetalle,
                            IdMovimientoStock = det.IdMovimientoStock,
                            IdArticulo = det.IdArticulo,
                            IdPosta = det.IdPosta,
                            Codigo = articulo?.Codigo ?? string.Empty,
                            Descripcion = articulo?.Descripcion ?? string.Empty,
                            Cantidad = det.Cantidad,
                            PrecioUnitario = det.PrecioUnitario,
                            PrecioTotal = det.PrecioTotal
                        });
                    }
                    _view.CargarDetalles(detallesDto);

                    List<MovimientoComprobante> comprobantes = await _comprobanteRepositorio.ObtenerPorMovimientoAsync(_movimiento.IdMovimientoStock);
                    var comprobantesDto = new List<MovimientoComprobanteDto>();
                    foreach (var det in comprobantes)
                    {
                        var proveedor = await _articuloProveedorRepositorio.ObtenerPorIdAsync(det.IdProveedor);
                        var tiposComprobantes = await _comprobanteRepositorio.ObtenerTiposComprobantesPorId(det.IdTipoComprobante);
                        comprobantesDto.Add(new MovimientoComprobanteDto
                        {
                            IdMovimientoComprobante = det.IdMovimientoComprobante,
                            IdMovimientoStock = det.IdMovimientoStock,
                            TipoComprobanteNombre = tiposComprobantes.Nombre,
                            NroComprobante = det.NroComprobante,
                            ProveedorNombre = proveedor?.RazonSocial ?? string.Empty,
                            RutaComprobante = det.RutaComprobante
                        });
                    }
                    _view.CargarComprobantes(comprobantesDto);
                }
            });

            ActualizarEstadoUI();
        }

        public async Task AbrirRequerimientoCompras(List<MovimientoStockDetalleDto> detalles, List<MovimientoComprobanteDto> comprobantes)
        {
            List<RcDetalleRcc> rcDetalles = detalles.Select(d => new RcDetalleRcc
            {
                Descripcion = $"{d.Codigo} - {d.Descripcion} - {d.PrecioTotal:C2}",
                Cantidad = d.Cantidad,
                Activo = true
            }).ToList();

            decimal precioTotal = detalles.Sum(d => d.PrecioTotal);

            Posta? posta = await _postaRepositorio.ObtenerPorIdAsync(_sesionService.IdPosta);

            await AbrirFormularioAsync<CrearRequerimientoForm>(async form =>
            {
                await form._presenter.InicializarAsync();
                form._presenter.AgregarDatosDesdeAntes(rcDetalles, precioTotal.ToString(), posta.Descripcion);
            });
        }

        private void ActualizarEstadoUI()
        {
            if (_movimiento == null)
                return;

            // 🔹 Caso 1: NO autorizado
            if (!_movimiento.Autorizado)
            {
                _view.HabilitarAutorizar(true);
                _view.HabilitarConfirmar(false);
                _view.HabilitarFechaEmision(true);
                _view.HabilitarFechaIngreso(false);
                return;
            }

            // 🔹 Caso 2: Autorizado pero sin ingreso
            if (_movimiento.Autorizado && _movimiento.FechaIngreso == null)
            {
                _view.HabilitarAutorizar(false);
                _view.HabilitarConfirmar(true);
                _view.HabilitarFechaEmision(false);
                _view.HabilitarFechaIngreso(false);
                return;
            }

            // 🔹 Caso 3: Autorizado y con ingreso
            if (_movimiento.Autorizado && _movimiento.FechaIngreso != null)
            {
                _view.HabilitarAutorizar(false);
                _view.HabilitarConfirmar(false);
                _view.HabilitarFechaEmision(false);
                _view.HabilitarFechaIngreso(true);
            }
        }

        public async void Autorizar()
        {
            _view.Autorizado = true;
            _view.MostrarMensaje("Movimiento autorizado.");
            await GuardarAsync(false);
            ActualizarEstadoUI();
        }

        public async Task ConfirmarIngresoAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                if (_movimiento == null)
                    throw new InvalidOperationException("Movimiento no inicializado.");

                _view.FechaIngreso = DateTime.Now;

                List<MovimientoStockDetalleDto> detalles = _view.ObtenerDetalles();

                foreach (var det in detalles)
                {
                    //Articulo? articulo = await _articuloRepositorio.ObtenerPorIdAsync(det.IdArticulo);  AL FINAL ESTA PUESTO CUANDO SE AGREGA
                    //if(articulo != null)                                                                EL ARTICULO AL GRID DE LOS ARTICULOS
                    //{
                    //    articulo.PrecioUnitario = det.PrecioUnitario;

                    //    await _articuloRepositorio.ActualizarArticuloAsync(articulo);
                    //}
            
                    var stock = await _articuloStockRepositorio.ObtenerStockAsync(det.IdArticulo, det.IdPosta);

                    if (stock != null)
                    {
                        if (_view.IdTipoMovimiento == 1)
                        {
                            stock.CantidadActual = (stock.CantidadActual ?? 0) + det.Cantidad;
                        }
                        else
                        {
                            stock.CantidadActual = (stock.CantidadActual ?? 0) - det.Cantidad;
                        }
                        await _articuloStockRepositorio.ActualizarStockAsync(stock);
                    }
                    else
                    {
                        await _articuloStockRepositorio.CrearStockAsync(det.IdArticulo, det.IdPosta, det.Cantidad);
                    }
                }

                _view.MostrarMensaje("Ingreso confirmado y stock actualizado.");
            });

            await GuardarAsync(false);
            ActualizarEstadoUI();
        }

        public async Task GuardarAsync(bool manual)
        {
            await EjecutarConCargaAsync(async () =>
            {
                if (_movimiento == null)
                    throw new InvalidOperationException("Movimiento no inicializado.");

                _movimiento.IdTipoMovimiento = _view.IdTipoMovimiento;
                _movimiento.FechaEmision = _view.FechaEmision;
                _movimiento.FechaIngreso = _view.FechaIngreso;
                _movimiento.Autorizado = _view.Autorizado;
                _movimiento.Observaciones = _view.Observaciones;

                await _movimientoRepositorio.ActualizarAsync(_movimiento);
                if (manual == true)
                {
                    _view.MostrarMensaje("Movimiento actualizado correctamente.");
                    _view.Cerrar();
                }
            });
        }

        public async Task EditarArtAsync(int idMovimientoDetalle)
        {
            await GuardarAsync(false);
            MovimientoStockDetalle? detalle = await _detalleRepositorio.ObtenerPorIdAsync(idMovimientoDetalle);
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarArtForm>(async form =>
                {
                    await form._presenter.InicializarAsync(_movimiento.IdMovimientoStock, detalle);
                });
            }, async () => await InicializarAsync(_movimiento.IdMovimientoStock));
        }

        public async Task EditarComprobanteAsync(int idMovimientoComprobante)
        {
            await GuardarAsync(false);
            MovimientoComprobante? detalle = await _comprobanteRepositorio.ObtenerPorIdAsync(idMovimientoComprobante);
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarComprobanteForm>(async form =>
                {
                    await form._presenter.InicializarAsync(_movimiento.IdMovimientoStock, "MovimientoStock", detalle);
                });
            }, async () => await InicializarAsync(_movimiento.IdMovimientoStock));
        }

        public async Task EliminarArtAsync(int idMovimientoDetalle)
        {
            await GuardarAsync(false);
            await EjecutarConCargaAsync(async () =>
            {
                await _detalleRepositorio.EliminarAsync(idMovimientoDetalle);
            }, async () => await InicializarAsync(_movimiento.IdMovimientoStock));
        }

        public async Task EliminarComprobanteAsync(int idMovimientoComprobante)
        {
            await GuardarAsync(false);
            await EjecutarConCargaAsync(async () =>
            {
                await _comprobanteRepositorio.EliminarAsync(idMovimientoComprobante);
            }, async () => await InicializarAsync(_movimiento.IdMovimientoStock));
        }

        public async Task AgregarArtAsync()
        {
            await GuardarAsync(false);
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarArtForm>(async form =>
                {
                    await form._presenter.InicializarAsync(_movimiento.IdMovimientoStock);
                });
            }, async () => await InicializarAsync(_movimiento.IdMovimientoStock));
        }

        public async Task AgregarComprobanteAsync()
        {
            await GuardarAsync(false);
            await EjecutarConCargaAsync(async () =>
            {
                await AbrirFormularioAsync<AgregarEditarComprobanteForm>(async form =>
                {
                    await form._presenter.InicializarAsync(_movimiento.IdMovimientoStock, "MovimientoStock");
                });
            }, async () => await InicializarAsync(_movimiento.IdMovimientoStock));
        }
    }
}