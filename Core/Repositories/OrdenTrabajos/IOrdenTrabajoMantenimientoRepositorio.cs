using Shared.Models;

namespace Core.Repositories
{
    public interface IOrdenTrabajoMantenimientoRepositorio
    {
        Task<int> AgregarAsync(OrdenTrabajoMantenimiento entidad);

        Task<int> ActualizarAsync(OrdenTrabajoMantenimiento entidad);

        Task<int> EliminarAsync(int id);

        Task<OrdenTrabajoMantenimiento?> ObtenerPorIdAsync(int id);

        Task<List<OrdenTrabajoMantenimiento>> ObtenerPorOrdenTrabajoAsync(int idOrdenTrabajo);
    }
}