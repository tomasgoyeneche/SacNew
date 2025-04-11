using Shared.Models;

namespace Core.Repositories
{
    public interface ICoberturaRepositorio
    {
        // Metodos de busqueda por id o general
        Task<List<Cobertura>> ObtenerTodasAsync();
    }
}