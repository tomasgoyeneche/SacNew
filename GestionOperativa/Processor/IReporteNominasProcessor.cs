using Core.Reports;
using GestionOperativa.Reports;

namespace GestionOperativa.Processor
{
    public interface IReporteNominasProcessor
    {
        Task<ReporteNominaActual?> ObtenerReporteNominaActual();

        Task<ReporteNominaMetanolActiva?> ObtenerReporteNominaMetanol();
    }
}