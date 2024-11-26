using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IConceptoProveedorRepositorio
    {
        Task<List<Proveedor>> ObtenerTodosLosProveedoresAsync();
    }
}