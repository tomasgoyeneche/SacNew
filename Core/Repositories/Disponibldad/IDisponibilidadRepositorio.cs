using Shared.Models;

namespace Core.Repositories
{
    public interface IDisponibilidadRepositorio
    {
        Task<List<Disponibilidad>> BuscarDisponiblesPorFechaAsync(DateTime fecha);

        Task<Disponible?> ObtenerDisponiblePorNominaYFechaAsync(int idNomina, DateTime fechaDisponible);

        Task<List<int>> ObtenerCuposUsadosAsync(int idOrigen, DateTime fechaDisponible);

        Task<List<DisponibleEstado>> ObtenerEstadosDeBajaAsync();

        Task ActualizarDisponibleAsync(Disponible disp);

        Task<Disponible?> ObtenerPorIdAsync(int idDisponible);

        Task AgregarDisponibleAsync(Disponible disp);

        Task<DisponibleEstado?> ObtenerEstadoDeBajaPorIdAsync(int idMotivo);

        Task<List<DisponibilidadYPF>> ObtenerDisponibilidadYPFPorFechaAsync(DateTime dispoFecha);

        Task<List<DisponibleFecha>> ObtenerProximasFechasDisponiblesAsync(DateTime desde, int cantidad);
    }
}