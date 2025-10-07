using Core.Base;
using Core.Repositories;
using Core.Services;
using Servicios.Views.Mantenimiento;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Presenters
{
    public class AgregarEditarMovimientoStockDetallePresenter
     : BasePresenter<IAgregarEditarMovimientoStockDetalleView>
    {
        private readonly IArticuloRepositorio _articuloRepositorio;
        private readonly IMovimientoStockDetalleRepositorio _detalleRepositorio;
        private readonly ISesionService _sesionService;

        public MovimientoStockDetalle? DetalleActual { get; private set; }

        private readonly int _idMovimientoStock;

        public AgregarEditarMovimientoStockDetallePresenter(
            IArticuloRepositorio articuloRepositorio,
            IMovimientoStockDetalleRepositorio detalleRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _articuloRepositorio = articuloRepositorio;
            _detalleRepositorio = detalleRepositorio;
            _sesionService = sesionService;
        }

        public async Task InicializarAsync(int idMovimientoStock, MovimientoStockDetalle? detalle = null)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var articulos = await _articuloRepositorio.ObtenerArticulosActivosAsync();
                _view.CargarArticulos(articulos);

                _view.IdMovimientoStock = idMovimientoStock;
                _view.IdPosta = _sesionService.IdPosta;
            });

                if (detalle != null)
                {
                    DetalleActual = detalle;
                    _view.SuspenderEventoArticulo();

                    _view.IdArticulo = detalle.IdArticulo;
                    _view.IdMovimientoStock = detalle.IdMovimientoStock;
                    _view.IdPosta = detalle.IdPosta;
                    _view.Cantidad = detalle.Cantidad;
                    _view.PrecioUnitario = detalle.PrecioUnitario;
                    _view.PrecioTotal = detalle.PrecioTotal;

                    Articulo articulo = await _articuloRepositorio.ObtenerPorIdAsync(detalle.IdArticulo);
                    if (articulo != null)
                        articulo.PrecioUnitario = 0;
                        _view.MostrarArticuloSeleccionado(articulo);
                // ⚡ lo reactivo después
                    _view.ReanudarEventoArticulo();
            }
           
        }

        public async Task ArticuloSeleccionadoAsync(int idArticulo)
        {
            var articulo = await _articuloRepositorio.ObtenerPorIdAsync(idArticulo);
            if (articulo != null)
                _view.MostrarArticuloSeleccionado(articulo);
        }

        public async Task GuardarAsync()
        {
            var detalle = new MovimientoStockDetalle
            {
                IdMovimientoDetalle = DetalleActual?.IdMovimientoDetalle ?? 0,
                IdMovimientoStock = _view.IdMovimientoStock,
                IdArticulo = _view.IdArticulo,
                IdPosta = _view.IdPosta,
                Cantidad = _view.Cantidad,
                PrecioUnitario = _view.PrecioUnitario,
                PrecioTotal = _view.PrecioTotal,
                Activo = true
            };

            await EjecutarConCargaAsync(async () =>
            {
                if (DetalleActual == null)
                {
                    var id = await _detalleRepositorio.AgregarAsync(detalle);
                    _view.MostrarMensaje("Detalle agregado correctamente.");
                }
                else
                {
                    await _detalleRepositorio.ActualizarAsync(detalle);
                    _view.MostrarMensaje("Detalle actualizado correctamente.");
                }
                _view.Cerrar();
            });
        }
    }

}
