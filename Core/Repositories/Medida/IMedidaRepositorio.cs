using Shared.Models;

namespace Core.Repositories
{
    public interface IMedidaRepositorio
    {
        Task<List<Medida>> ObtenerTodosAsync();
    }
}