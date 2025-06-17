using Shared.Models;

namespace Core.Repositories
{
    public interface INominaRepositorio
    {
        Task<Nomina?> ObtenerNominaActivaPorUnidadAsync(int idUnidad, DateTime fechaReferencia);

        Task<Nomina?> ObtenerNominaActivaPorChoferAsync(int idChofer, DateTime fechaReferencia);

        Task<Nomina?> ObtenerPorIdAsync(int idNomina);

        Task RegistrarNominaAsync(int idNomina, string evento, string descripcion, int idUsuario);

        Task<List<VencimientosDto>> ObtenerVencimientosPorNominaAsync(Nomina nomina);

        Task CambiarChoferUnidadAsync(int? idChofer, int idUnidad, DateTime fecha);
    }
}