using Shared.Models;

namespace Core.Repositories
{
    public interface IArticuloFamiliaRepositorio
    {
        Task<List<ArticuloFamilia>> ObtenerTodasAsync();

        Task<ArticuloFamilia?> ObtenerPorIdAsync(int id);

        Task<int> AgregarAsync(ArticuloFamilia familia);

        Task<int> ActualizarAsync(ArticuloFamilia familia);

        Task<int> EliminarAsync(int id);
    }
}