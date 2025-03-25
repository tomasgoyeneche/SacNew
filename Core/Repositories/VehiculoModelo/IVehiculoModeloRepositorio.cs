using Shared.Models;

namespace Core.Repositories
{
    public interface IVehiculoModeloRepositorio
    {
        Task<List<VehiculoModelo>> ObtenerModelosPorMarcaAsync(int idMarca);
    }
}