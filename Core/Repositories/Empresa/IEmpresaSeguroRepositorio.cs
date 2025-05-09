using Shared.Models;

namespace Core.Repositories
{
    public interface IEmpresaSeguroRepositorio
    {
        Task<List<EmpresaSeguroDto>> ObtenerSegurosPorEmpresaAsync(int idEmpresa);

        Task<List<EmpresaSeguroDto?>> ObtenerSeguroPorEmpresaYEntidadAsync(int idEmpresa, int idEmpresaSeguroEntidad);

        Task<EmpresaSeguro?> ObtenerSeguroPorIdAsync(int idEmpresaSeguro);

        Task AgregarSeguroAsync(EmpresaSeguro seguro);

        Task ActualizarSeguroAsync(EmpresaSeguro seguro);

        Task EliminarSeguroAsync(int idEmpresaSeguro);
    }
}