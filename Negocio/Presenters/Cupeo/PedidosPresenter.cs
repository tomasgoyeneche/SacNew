using Core.Base;
using Core.Repositories;
using Core.Services;
using GestionFlota.Views;
using Shared.Models;

namespace GestionFlota.Presenters
{
    public class PedidosPresenter : BasePresenter<IPedidosView>
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IUnidadRepositorio _unidadRepositorio;

        public PedidosPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IPedidoRepositorio pedidoRepositorio,
            IUnidadRepositorio unidadRepositorio)
            : base(sesionService, navigationService)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _unidadRepositorio = unidadRepositorio;
        }

        public async Task InicializarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<PedidoDto> pedidos = await _pedidoRepositorio.ObtenerPedidosPendientes();

                _view.CargarPedidos(pedidos);
            });
        }

        public async Task EliminarPedido(int idPedido)
        {
            await EjecutarConCargaAsync(async () =>
            {
                var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar este Pedido?");
                if (confirmacion != DialogResult.Yes) return;

                await _pedidoRepositorio.EliminarPedidoAsync(idPedido);

                _view.MostrarMensaje("Pedido eliminado correctamente.");
            }, async () => await InicializarAsync());
        }
    }
}