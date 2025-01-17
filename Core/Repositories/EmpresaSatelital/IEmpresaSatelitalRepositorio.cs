using Shared.Models;

namespace Core.Repositories
{
    public interface IEmpresaSatelitalRepositorio
    {
        Task<List<EmpresaSatelitalDto>> ObtenerSatelitalesPorEmpresaAsync(int idEmpresa);
    }
}