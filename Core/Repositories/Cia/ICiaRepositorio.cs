using Shared.Models;

namespace Core.Repositories
{
    public interface ICiaRepositorio
    {
        // Metodos de busqueda general o por id
        Task<List<Cia>> ObtenerTodasAsync();

        // Metodos de busqueda especificos

        Task<List<Cia>> ObtenerPorTipoAsync(int idEmpresaSeguroEntidad);
    }
}