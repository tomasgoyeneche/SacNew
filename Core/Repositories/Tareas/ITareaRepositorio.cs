using Shared.Models;

namespace Core.Repositories
{
    public interface ITareaRepositorio
    {
        Task<int> AgregarAsync(Tarea tarea);

        Task<int> ActualizarAsync(Tarea tarea);

        Task<int> EliminarAsync(int idTarea);

        Task<Tarea?> ObtenerPorIdAsync(int idTarea);

        Task<List<Tarea>> ObtenerTodosAsync();
    }
}