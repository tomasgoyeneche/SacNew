using Shared.Models;

namespace Core.Repositories
{
    public interface IArticuloProveedorRepositorio
    {
        Task<int> AgregarAsync(ArticuloProveedor proveedor);

        Task<int> ActualizarAsync(ArticuloProveedor proveedor);

        Task<int> EliminarAsync(int idProveedor);

        Task<ArticuloProveedor?> ObtenerPorIdAsync(int idProveedor);

        Task<List<ArticuloProveedor>> ObtenerTodosAsync();
    }
}