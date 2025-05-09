using Shared.Models;

namespace Core.Repositories
{
    public interface IVersionRepositorio
    {
        Task<Versiones?> ObtenerVersionActivaAsync();

        Task<Versiones?> ObtenerPorNumeroAsync(string numeroVersion);

        Task InsertarVersionAsync(Versiones version);

        Task ActualizarVersionAsync(Versiones version);
    }
}