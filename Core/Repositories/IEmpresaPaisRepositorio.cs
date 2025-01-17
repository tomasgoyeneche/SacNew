using Shared.Models;

namespace Core.Repositories
{
    public interface IEmpresaPaisRepositorio
    {
        Task<List<EmpresaPaisDto>> ObtenerPaisesPorEmpresaAsync(int idEmpresa);
    }
}