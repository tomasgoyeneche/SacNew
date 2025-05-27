using Shared.Models;

namespace Core.Repositories
{
    public interface ITeRepositorio
    {
        Task<TransitoEspecial?> ObtenerPorIdAsync(int id);
    }
}