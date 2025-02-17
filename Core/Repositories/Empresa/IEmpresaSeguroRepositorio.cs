using Shared.Models;

namespace Core.Repositories
{
    public interface IEmpresaSeguroRepositorio
    {
        Task<EmpresaSeguro?> ObtenerPorEmpresaAsync(int idEmpresa);

        Task ActualizarAsync(EmpresaSeguro seguro);
    }
}