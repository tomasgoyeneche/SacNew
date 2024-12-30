using Shared.Models.DTOs;

namespace Core.Repositories
{
    public interface IUnidadRepositorio
    {
        Task<int?> ObtenerIdTractorPorPatenteAsync(string patente);

        Task<int?> ObtenerIdUnidadPorTractorAsync(int idTractor);

        Task<List<UnidadPatenteDto>> ObtenerUnidadesPatenteDtoAsync();

        Task<UnidadPatenteDto?> ObtenerPorIdAsync(int idUnidad);
    }
}