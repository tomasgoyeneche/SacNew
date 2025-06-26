using Shared.Models;

namespace Core.Repositories
{
    public interface ILocacionRepositorio
    {
        Task<List<Locacion>> ObtenerTodasAsync();

        Task<List<Locacion>> BuscarPorCriterioAsync(string criterio);

        Task EliminarAsync(int idLocacion);

        Task<Locacion?> ObtenerPorIdAsync(int idLocacion);

        Task AgregarAsync(Locacion locacion);

        Task ActualizarAsync(Locacion locacion);

        Task<List<LocacionSinonimo>> ObtenerTodosSinonimosAsync();

        Task AgregarSinonimoAsync(LocacionSinonimo sinonimo);
    }
}