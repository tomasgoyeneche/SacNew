using Shared.Models;

namespace Core.Repositories
{
    public interface ILocacionPoolRepositorio
    {
        Task<IEnumerable<Locacion>> ObtenerPorIdPoolAsync(int idPool);

        Task<IEnumerable<Locacion>> ObtenerLocacionesDisponiblesAsync();

        Task<LocacionPool?> ObtenerRelacionAsync(int idPool, int idLocacion);

        Task AgregarLocacionAlPoolAsync(LocacionPool locacionPool);

        Task EliminarLocacionDelPoolAsync(int idLocacionPool);
    }
}