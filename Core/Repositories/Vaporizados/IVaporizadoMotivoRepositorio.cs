using Shared.Models;

namespace Core.Repositories
{
    public interface IVaporizadoMotivoRepositorio
    {
        Task<List<VaporizadoMotivo>> ObtenerTodosAsync();
    }
}