using Shared.Models;

namespace Core.Repositories
{
    public interface IConfRepositorio
    {
        Task<Conf?> ObtenerRutaPorIdAsync(int idConf);
    }
}