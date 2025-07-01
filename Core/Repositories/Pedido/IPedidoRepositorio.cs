using Shared.Models;

namespace Core.Repositories
{
    public interface IPedidoRepositorio
    {
        Task<List<Cupeo>> ObtenerCupeoAsync();

        Task<List<PedidoDto>> ObtenerPedidosPendientes();
        Task EliminarPedidoAsync(int idPedido);

        Task InsertarPedidosAsync(IEnumerable<Pedido> pedidos);
    }
}