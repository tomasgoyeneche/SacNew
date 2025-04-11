using Shared.Models;

namespace Core.Repositories
{
    public interface ILocalidadRepositorio
    {
        Task<List<Localidad>> ObtenerPorProvinciaAsync(int idProvincia);

        Task<int> ObtenerPorIdAsync(int idLocalidad);
    }
}