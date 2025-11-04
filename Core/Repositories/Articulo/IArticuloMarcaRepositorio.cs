using Shared.Models;

namespace Core.Repositories
{
    public interface IArticuloMarcaRepositorio
    {
        Task<List<ArticuloMarca>> ObtenerTodasAsync();

        Task<ArticuloMarca?> ObtenerPorIdAsync(int id);

        Task<int> AgregarAsync(ArticuloMarca marca);

        Task<int> ActualizarAsync(ArticuloMarca marca);

        Task<int> EliminarAsync(int id);
    }
}