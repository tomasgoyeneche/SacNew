using Shared.Models;

namespace Core.Repositories
{
    public interface IEmpresaSatelitalRepositorio
    {
        Task<List<EmpresaSatelitalDto>> ObtenerSatelitalesPorEmpresaAsync(int idEmpresa);

        Task AgregarAsync(EmpresaSatelital empresaSatelital);

        Task EliminarAsync(int idEmpresaSatelital);

        Task<int?> ObtenerIdEmpresaSatelitalAsync(int idEmpresa, int idSatelital);

    }
}