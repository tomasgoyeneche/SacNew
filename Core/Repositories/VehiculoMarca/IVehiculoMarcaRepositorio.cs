using Shared.Models;

namespace Core.Repositories
{
    public interface IVehiculoMarcaRepositorio
    {
        Task<List<VehiculoMarca>> ObtenerMarcasPorTipoAsync(int tipoVehiculo);
    }
}