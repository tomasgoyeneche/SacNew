using Shared.Models;

namespace Core.Repositories
{
    public interface IMantenimientoTareaRepositorio
    {
        Task<int> AgregarAsync(MantenimientoTarea mantenimientoTarea);

        Task<int> EliminarAsync(int idMantenimientoTarea);

        Task<List<MantenimientoTarea>> ObtenerPorMantenimientoAsync(int idMantenimiento);
    }
}