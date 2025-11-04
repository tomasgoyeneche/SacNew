using Shared.Models;

namespace Core.Repositories
{
    public interface IOrdenTrabajoTareaRepositorio
    {
        Task<int> AgregarAsync(OrdenTrabajoTarea entidad);

        Task<int> ActualizarAsync(OrdenTrabajoTarea entidad);

        Task<int> EliminarAsync(int id);

        Task<OrdenTrabajoTarea?> ObtenerPorIdAsync(int id);

        Task<List<OrdenTrabajoTarea>> ObtenerPorMantenimientoAsync(int idOrdenTrabajoMantenimiento);
    }
}