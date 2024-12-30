using Shared.Models;

namespace Core.Repositories
{
    public interface IConceptoProveedorRepositorio
    {
        Task<List<Proveedor>> ObtenerTodosLosProveedoresAsync();
    }
}