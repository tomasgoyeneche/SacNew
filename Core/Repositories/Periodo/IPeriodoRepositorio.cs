using Shared.Models;

namespace Core.Repositories
{
    public interface IPeriodoRepositorio
    {
        Task<List<Periodo>> ObtenerPeriodosActivosAsync();
    }
}