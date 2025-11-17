using Shared.Models;

namespace Core.Repositories
{
    public interface IOrdenTrabajoRepositorio
    {
        Task<int> AgregarAsync(OrdenTrabajo orden);

        Task<int> ActualizarAsync(OrdenTrabajo orden);

        Task<int> EliminarAsync(int idOrdenTrabajo);

        Task<OrdenTrabajo?> ObtenerPorIdAsync(int idOrdenTrabajo);

        Task<List<OrdenTrabajo>> ObtenerTodosAsync();

        Task<List<OrdenTrabajoDto>> ObtenerTodosDtoAsync();

        Task<List<OrdenTrabajoDto>> ObtenerPorDtoFaseAsync(string fase);

        Task<List<OrdenTrabajoDto>> ObtenerNoCoincidetesDtoFaseAsync(string fase);

        Task<List<OrdenTrabajoProximoDto>> ObtenerOrdenTrabajoProximoAsync();

        Task<List<OrdenTrabajo>> ObtenerPorNominaAsync(int idNomina);
    }
}