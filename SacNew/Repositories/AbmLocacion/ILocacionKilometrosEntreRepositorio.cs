using SacNew.Models;

namespace SacNew.Repositories
{
    public interface ILocacionKilometrosEntreRepositorio
    {
        Task<List<LocacionKilometrosEntre>> ObtenerPorLocacionIdAsync(int idLocacion);

        Task AgregarAsync(LocacionKilometrosEntre locacionKilometrosEntre);

        Task EliminarAsync(int idKilometros);
    }
}