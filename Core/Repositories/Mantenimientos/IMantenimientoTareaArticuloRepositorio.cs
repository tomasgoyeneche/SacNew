using Shared.Models;

namespace Core.Repositories
{
    public interface IMantenimientoTareaArticuloRepositorio
    {
        Task<int> AgregarAsync(MantenimientoTareaArticulo mantenimientoTareaArticulo);

        Task<int> EliminarAsync(int idMantenimientoTareaArticulo);

        Task<List<MantenimientoTareaArticulo>> ObtenerPorTareaAsync(int idTarea);

        Task<MantenimientoTareaArticulo?> ObtenerPorIdAsync(int idMantenimientoTareaArticulo);

        Task<MantenimientoTareaArticulo?> ObtenerPorTareaYArticuloAsync(int idTarea, int idArticulo);
    }
}