using Shared.Models;

namespace Core.Repositories
{
    public interface ICoberturaRepositorio
    {
        Task<List<Cobertura>> ObtenerTodasAsync();
    }
}