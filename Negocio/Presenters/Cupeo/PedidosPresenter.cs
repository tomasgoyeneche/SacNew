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
        private readonly IProgramaRepositorio _programaRepositorio;
        public PedidosPresenter(
            ISesionService sesionService,
            INavigationService navigationService,
            IPedidoRepositorio pedidoRepositorio,
            IProgramaRepositorio programaRepositorio,
            IUnidadRepositorio unidadRepositorio)
            : base(sesionService, navigationService)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _programaRepositorio = programaRepositorio;
            _unidadRepositorio = unidadRepositorio;
        }

        public async Task InicializarAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                List<PedidoDto> pedidos = await _pedidoRepositorio.ObtenerPedidosPendientes();

                // Lista final filtrada
                List<PedidoDto> pedidosFiltrados = new();

                foreach (var pedido in pedidos)
                {
                    // Si el albarán es 0 => lo dejamos siempre
                    if (pedido.AlbaranDespacho == "0")
                    {
                        pedidosFiltrados.Add(pedido);
                        continue;
                    }

                    // Caso contrario, consultamos en programaRepositorio
                    bool existe = await _programaRepositorio.ExisteAlbaranRepetidoAsync(Convert.ToInt32(pedido.AlbaranDespacho));

                    if (!existe)
                    {
                        pedidosFiltrados.Add(pedido);
                    }
                    // si existe => se ignora (lo sacás de la lista)
                }

                _view.CargarPedidos(pedidosFiltrados);
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