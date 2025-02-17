using Shared.Models;

namespace Core.Repositories
{
    public interface ISatelitalRepositorio
    {
        Task<List<Satelital>> ObtenerTodosAsync();
    }
}