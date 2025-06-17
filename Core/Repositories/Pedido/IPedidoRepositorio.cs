using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IPedidoRepositorio
    {
        Task<List<Cupeo>> ObtenerCupeoAsync();
        Task InsertarPedidosAsync(IEnumerable<Pedido> pedidos);
    }
}
