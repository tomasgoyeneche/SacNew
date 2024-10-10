using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IProductoRepositorio
    {
        Task<List<Producto>> ObtenerTodosAsync();
    }
}