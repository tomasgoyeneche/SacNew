using Shared.Models;

namespace Core.Repositories
{
    public interface IProductoRepositorio
    {
        Task<List<Producto>> ObtenerTodosAsync();
    }
}