using Shared.Models;

namespace Core.Repositories
{
    public interface ILocacionKilometrosEntreRepositorio
    {
        Task<List<LocacionKilometrosEntre>> ObtenerPorLocacionIdAsync(int idLocacion);

        Task AgregarAsync(LocacionKilometrosEntre locacionKilometrosEntre);

        Task EliminarAsync(int idKilometros);
    }
}