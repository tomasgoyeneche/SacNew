using Shared.Models;

namespace Core.Repositories
{
    public interface IEmpresaSeguroEntidadRepositorio
    {
        Task<List<EmpresaSeguroEntidad>> ObtenerTodasAsync();
    }
}