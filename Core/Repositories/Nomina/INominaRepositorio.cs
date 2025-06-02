using Shared.Models;

namespace Core.Repositories
{
    public interface INominaRepositorio
    {
        Task<Nomina?> ObtenerNominaActivaPorUnidadAsync(int idUnidad, DateTime fechaReferencia);

        Task<Nomina?> ObtenerNominaActivaPorChoferAsync(int idChofer, DateTime fechaReferencia);

        Task<Nomina?> ObtenerPorIdAsync(int idNomina);

        Task<List<VencimientosDto>> ObtenerVencimientosPorNominaAsync(Nomina nomina);
    }
}