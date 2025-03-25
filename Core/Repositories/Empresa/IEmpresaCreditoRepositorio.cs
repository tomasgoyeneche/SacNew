using Shared.Models;

namespace Core.Repositories
{
    public interface IEmpresaCreditoRepositorio
    {
        Task<EmpresaCredito> ObtenerPorEmpresaAsync(int idEmpresa);

        Task<decimal?> ObtenerCreditoPorEmpresaYPeriodoAsync(int idEmpresa, int idPeriodo);

        Task InsertarCreditoAsync(int idEmpresa, int idPeriodo, decimal credito);

        Task ActualizarCreditoAsync(EmpresaCredito empresaCredito);

        Task ActualizarCreditoPeriodoAsync(int idEmpresa, int idPeriodo, decimal credito);
    }
}