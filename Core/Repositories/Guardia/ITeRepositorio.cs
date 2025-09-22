using Shared.Models;

namespace Core.Repositories
{
    public interface ITeRepositorio
    {
        Task<TransitoEspecial?> ObtenerPorIdAsync(int id);
        Task<List<GuardiaTransitoEspecialDto>> ObtenerControlTransitoEspecial(DateTime desde, DateTime hasta);
    }
}