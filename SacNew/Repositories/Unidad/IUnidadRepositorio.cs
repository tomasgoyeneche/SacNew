using SacNew.Models.DTOs;

namespace SacNew.Repositories
{
    public interface IUnidadRepositorio
    {
        Task<int?> ObtenerIdTractorPorPatenteAsync(string patente);

        Task<int?> ObtenerIdUnidadPorTractorAsync(int idTractor);

        Task<List<UnidadPatenteDto>> ObtenerUnidadesPatenteDtoAsync();

        Task<UnidadPatenteDto?> ObtenerPorIdAsync(int idUnidad);
    }
}