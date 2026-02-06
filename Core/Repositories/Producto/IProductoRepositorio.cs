using Shared.Models;

namespace Core.Repositories
{
    public interface IProductoRepositorio
    {
        Task<List<Producto>> ObtenerTodosAsync();

        Task<List<ProductoSinonimo>> ObtenerTodosSinonimosAsync();

        Task AgregarSinonimoAsync(ProductoSinonimo sinonimo);
        Task<VaporizadoMotivo?> ObtenerPorIdVapMotivoAsync(int idVaporizadoMotivo);
        Task<Producto?> ObtenerPorIdAsync(int idProducto);
    }
}