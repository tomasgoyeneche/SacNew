using Shared.Models;

namespace Core.Repositories
{
    public interface IPeriodoRepositorio
    {

        Task<Periodo> ObtenerPorIdAsync(int idPeriodo); 

        Task<List<Periodo>> ObtenerPeriodosParaSeleccionAsync();

        Task<List<Periodo>> ObtenerPeriodosActivosAsync();

        Task<int?> ObtenerIdPeriodoPorMesAnioAsync(int mes, int anio);
    }
}