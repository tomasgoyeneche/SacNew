using SacNew.Models.DTOs;

namespace SacNew.Repositories
{
    public interface IUnidadRepositorio
    {
        Task<int?> ObtenerIdTractorPorPatenteAsync(string patente);

        Task<int?> ObtenerIdUnidadPorTractorAsync(int idTractor);

        public List<UnidadPatenteDto> ObtenerUnidadesPatenteDto();

        Task<UnidadPatenteDto?> ObtenerPorIdAsync(int idUnidad);
    }
}