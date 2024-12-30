using Shared.Models;

namespace Core.Repositories
{
    public interface IProvinciaRepositorio
    {
        Task<List<Provincia>> ObtenerProvinciasAsync();
    }
}