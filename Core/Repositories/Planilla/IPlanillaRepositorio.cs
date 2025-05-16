using Shared.Models;
using Shared.Models.DTOs;

namespace Core.Repositories
{
    public interface IPlanillaRepositorio
    {
        Task<List<PlanillaPreguntaDto>> ObtenerPreguntasPorPlanilla(int idPlanilla);

        Task<Planilla?> ObtenerPorIdAsync(int idChofer);
    }
}