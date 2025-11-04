using Shared.Models;

namespace Core.Repositories
{
    public interface IMovimientoStockRepositorio
    {
        Task<List<MovimientoStockDto>> ObtenerMovimientosAsync();

        Task<MovimientoStock?> ObtenerPorIdAsync(int id);

        Task<int> AgregarAsync(MovimientoStock movimiento);

        Task ActualizarAsync(MovimientoStock movimiento);

        Task<List<TipoMovimientoStock>> ObtenerTipoMovimientosAsync();
    }
}