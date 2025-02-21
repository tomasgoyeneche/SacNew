using Shared.Models;

namespace Core.Repositories
{
    public interface ICiaRepositorio
    {
        Task<List<Cia>> ObtenerTodasAsync();

        Task<List<Cia>> ObtenerPorTipoAsync(int idTipoCia);
    }
}