using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IMovimientoStockDetalleRepositorio
    {
        Task<List<MovimientoStockDetalle>> ObtenerPorMovimientoAsync(int idMovimientoStock);
        Task<int> AgregarAsync(MovimientoStockDetalle detalle);
        Task ActualizarAsync(MovimientoStockDetalle detalle);
        Task EliminarAsync(int idMovimientoDetalle);
        Task<MovimientoStockDetalle?> ObtenerPorIdAsync(int idMovimientoDetalle);
    }
}
