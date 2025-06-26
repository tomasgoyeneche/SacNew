using Shared.Models;

namespace Core.Repositories
{
    public interface IPedidoRepositorio
    {
        Task<List<Cupeo>> ObtenerCupeoAsync();

        Task InsertarPedidosAsync(IEnumerable<Pedido> pedidos);
    }
}