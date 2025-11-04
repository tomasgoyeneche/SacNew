using Shared.Models;

namespace Core.Repositories
{
    public interface ILugarReparacionRepositorio
    {
        Task<int> AgregarAsync(LugarReparacion entidad);

        Task<int> ActualizarAsync(LugarReparacion entidad);

        Task<int> EliminarAsync(int id);

        Task<LugarReparacion?> ObtenerPorIdAsync(int id);

        Task<List<LugarReparacion>> ObtenerTodosAsync();
    }
}