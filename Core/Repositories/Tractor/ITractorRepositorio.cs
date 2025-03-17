using Shared.Models;

namespace Core.Repositories
{
    public interface ITractorRepositorio
    {
        Task<List<TractorDto>> ObtenerTodosLosTractoresDto();

        Task<TractorDto> ObtenerPorIdDtoAsync(int idTractor);

        Task EliminarTractorAsync(int idTractor);

        Task<List<TractorDto>> BuscarTractoresAsync(string textoBusqueda);
    }
}