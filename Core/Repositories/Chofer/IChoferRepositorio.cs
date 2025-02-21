using Shared.Models;

namespace Core.Repositories
{
    public interface IChoferRepositorio
    {
        Task<int?> ObtenerIdPorDocumentoAsync(string documento);

        Task<List<Chofer>> ObtenerTodosLosChoferes();

        Task<List<ChoferDto>> ObtenerTodosLosChoferesDto();

        Task<ChoferDto> ObtenerPorIdDtoAsync(int idChofer);

        Task<Chofer> ObtenerPorIdAsync(int idChofer);

        Task<List<ChoferDto>> BuscarAsync(string textoBusqueda);

        Task EliminarChoferAsync(int idChofer);

        Task ActualizarAsync(Chofer chofer);
    }
}