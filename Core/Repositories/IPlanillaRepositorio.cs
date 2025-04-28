using Shared.Models;
using Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IPlanillaRepositorio
    {

        Task<List<PlanillaPreguntaDto>> ObtenerPreguntasPorPlanilla(int idPlanilla);

        Task<Planilla> ObtenerPorIdAsync(int idChofer);
    }
}
