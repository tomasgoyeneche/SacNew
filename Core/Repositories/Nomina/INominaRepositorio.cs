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

        Task<List<HistorialGeneralDto>> ObtenerHistorialPorNomina(int idNomina);

        Task<Nomina?> ObtenerNominaMasNuevaPorUnidad(int idUnidad);
        Task<Nomina?> ObtenerNominaMasNuevaPorChofer(int idChofer);

        Task CambiarChoferUnidadAsync(int? idChofer, int idUnidad, DateTime fecha, string Observaciones);
    }
}