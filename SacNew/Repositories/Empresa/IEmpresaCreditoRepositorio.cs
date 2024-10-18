using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IEmpresaCreditoRepositorio
    {
        Task<EmpresaCredito> ObtenerPorEmpresaAsync(int idEmpresa);

        Task ActualizarCreditoAsync(EmpresaCredito empresaCredito);

        Task AgregarCreditoAsync(EmpresaCredito empresaCredito);
    }
}