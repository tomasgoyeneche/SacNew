using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IDisponibilidadRepositorio
    {
        Task<List<Disponibilidad>> BuscarDisponiblesPorFechaAsync(DateTime fecha);
        Task<Disponible?> ObtenerDisponiblePorNominaYFechaAsync(int idNomina, DateTime fechaDisponible);
        Task<List<int>> ObtenerCuposUsadosAsync(int idOrigen, DateTime fechaDisponible);
        Task<List<DisponibleEstado>> ObtenerEstadosDeBajaAsync();
        Task ActualizarDisponibleAsync(Disponible disp);
        Task AgregarDisponibleAsync(Disponible disp);
    }
}
