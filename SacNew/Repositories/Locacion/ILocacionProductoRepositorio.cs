using SacNew.Models;

namespace SacNew.Repositories
{
    public interface ILocacionProductoRepositorio
    {
        Task<List<LocacionProducto>> ObtenerPorLocacionIdAsync(int idLocacion);

        Task AgregarAsync(LocacionProducto locacionProducto);

        Task EliminarAsync(int idLocacionProducto);
    }
}