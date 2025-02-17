using Shared.Models;

namespace Core.Repositories
{
    public interface ICiaRepositorio
    {
        Task<List<Cia>> ObtenerTodasAsync();
    }
}