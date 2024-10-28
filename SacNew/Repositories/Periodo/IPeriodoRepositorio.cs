using SacNew.Models;

namespace SacNew.Repositories
{
    public interface IPeriodoRepositorio
    {
        Task<List<Periodo>> ObtenerPeriodosActivosAsync();
    }
}