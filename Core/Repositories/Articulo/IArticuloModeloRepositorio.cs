using Shared.Models;

namespace Core.Repositories
{
    public interface IArticuloModeloRepositorio
    {
        Task<List<ArticuloModelo>> ObtenerTodasAsync();

        Task<List<ArticuloModelo>> ObtenerPorMarcaAsync(int idMarca);

        Task<ArticuloModelo?> ObtenerPorIdAsync(int id);

        Task<int> AgregarAsync(ArticuloModelo modelo);

        Task<int> ActualizarAsync(ArticuloModelo modelo);

        Task<int> EliminarAsync(int id);
    }
}