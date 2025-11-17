using Shared.Models;

namespace Core.Repositories
{
    public interface IMovimientoStockRepositorio
    {
        Task<List<MovimientoStockDto>> ObtenerMovimientosAsync();

        Task<MovimientoStock?> ObtenerPorIdAsync(int id);

        Task<int> AgregarAsync(MovimientoStock movimiento);

        Task ActualizarAsync(MovimientoStock movimiento);

        Task<MovimientoStock?> ObtenerPorFechaEmisionAsync(DateTime date);

        Task<List<TipoMovimientoStock>> ObtenerTipoMovimientosAsync();

        Task<List<ArticuloStockDepositoDto>> ObtenerStockPorPostaAsync(int idPosta);

        Task<List<ArticuloStockDepositoDto>> ObtenerStockPorPostaCriticoAsync(int idPosta);

        Task<List<ArticuloMovimientoHistoricoDto>> ObtenerMovimientosPorPostaAsync(int idPosta);
    }
}