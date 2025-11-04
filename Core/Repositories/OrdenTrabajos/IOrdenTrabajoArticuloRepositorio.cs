using Shared.Models;

namespace Core.Repositories
{
    public interface IOrdenTrabajoArticuloRepositorio
    {
        Task<int> AgregarAsync(OrdenTrabajoArticulo entidad);

        Task<int> ActualizarAsync(OrdenTrabajoArticulo entidad);

        Task<int> EliminarAsync(int id);

        Task<OrdenTrabajoArticulo?> ObtenerPorIdAsync(int id);

        Task<List<OrdenTrabajoArticulo>> ObtenerPorTareaAsync(int idOrdenTrabajoTarea);

        Task<OrdenTrabajoArticulo?> ObtenerPorTareaYArticuloAsync(int idTarea, int idArticulo);
    }
}