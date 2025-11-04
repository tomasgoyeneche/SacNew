using Shared.Models;

namespace Core.Repositories
{
    public interface ITipoMantenimientoRepositorio
    {
        Task<List<TipoMantenimiento>> ObtenerTodosAsync();
    }
}