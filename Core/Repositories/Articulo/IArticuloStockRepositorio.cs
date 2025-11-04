using Shared.Models;

namespace Core.Repositories
{
    public interface IArticuloStockRepositorio
    {
        Task<int> CrearStockAsync(int idArticulo, int idPosta, decimal cantidadInicial = 0);

        Task<ArticuloStock?> ObtenerStockAsync(int idArticulo, int idPosta);

        Task<int> ActualizarStockAsync(ArticuloStock stock);
    }
}