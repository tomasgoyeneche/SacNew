using Shared.Models;

namespace Core.Repositories
{
    public interface IVaporizadoZonaRepositorio
    {
        Task<List<VaporizadoZona>> ObtenerTodasAsync();
    }
}