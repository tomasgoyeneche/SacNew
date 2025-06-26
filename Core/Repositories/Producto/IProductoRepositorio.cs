using Shared.Models;

namespace Core.Repositories
{
    public interface IProductoRepositorio
    {
        Task<List<Producto>> ObtenerTodosAsync();

        Task<List<ProductoSinonimo>> ObtenerTodosSinonimosAsync();

        Task AgregarSinonimoAsync(ProductoSinonimo sinonimo);

        Task<Producto?> ObtenerPorIdAsync(int idProducto);
    }
}