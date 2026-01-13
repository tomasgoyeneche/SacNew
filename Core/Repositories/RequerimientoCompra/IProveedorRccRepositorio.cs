using Shared.Models.RequerimientoCompra;

namespace Core.Repositories.RequerimientoCompra
{
    public interface IProveedorRccRepositorio
    {
        Task<List<ProveedorRcc>> ObtenerActivosAsync();

        Task<ProveedorRcc?> ObtenerPorIdAsync(int idProveedor);
    }
}