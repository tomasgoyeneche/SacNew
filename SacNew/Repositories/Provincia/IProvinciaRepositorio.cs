using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IProvinciaRepositorio
    {
        Task<List<Provincia>> ObtenerProvinciasAsync();
    }
}