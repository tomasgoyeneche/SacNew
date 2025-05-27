using Shared.Models;

namespace Core.Repositories
{
    public interface IGuardiaIngresoOtrosRepositorio
    {
        Task<GuardiaIngresoOtros?> ObtenerPorIdAsync(int id);

        Task<int> RegistrarIngresoOtrosAsync(GuardiaIngresoOtros ingresoOtros, int idPosta, DateTime fechaIngreso, int idUsuario);
    }
}