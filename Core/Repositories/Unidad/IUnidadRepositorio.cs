using Shared.Models;
using Shared.Models.DTOs;

namespace Core.Repositories
{
    public interface IUnidadRepositorio
    {
        Task<int?> ObtenerIdTractorPorPatenteAsync(string patente);

        Task<int?> ObtenerIdUnidadPorTractorAsync(int idTractor);

        Task<List<UnidadPatenteDto>> ObtenerUnidadesPatenteDtoAsync();

        Task<UnidadDto> ObtenerPorIdDtoAsync(int idUnidad);

        Task EliminarUnidadAsync(int idUnidad);

        Task<List<UnidadDto>> ObtenerUnidadesDtoAsync();

        Task<UnidadPatenteDto?> ObtenerPorIdAsync(int idUnidad);

        Task<List<NominaMetanolActivaDto>> ObtenerNominaMetanolActiva();
    }
}