using Shared.Models;

namespace Core.Repositories
{
    public interface IEmpresaCreditoRepositorio
    {
        Task<EmpresaCredito> ObtenerPorEmpresaAsync(int idEmpresa);

        Task ActualizarCreditoAsync(EmpresaCredito empresaCredito);

        Task AgregarCreditoAsync(EmpresaCredito empresaCredito);
    }
}