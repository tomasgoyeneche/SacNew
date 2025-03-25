using Shared.Models;

namespace Core.Repositories
{
    public interface IPeriodoRepositorio
    {
        Task<List<Periodo>> ObtenerPeriodosParaSeleccionAsync();

        Task<List<Periodo>> ObtenerPeriodosActivosAsync();

        Task<int?> ObtenerIdPeriodoPorMesAnioAsync(int mes, int anio);
    }
}