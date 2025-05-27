using GestionDocumental.Reports;
using Shared.Models;

namespace GestionOperativa.Processor
{
    public interface IReporteConsumosNomTeOtrosProcessor
    {
        Task<ReporteControlOperativoConsumos?> ObtenerReporteConsumosNomina(int idNomina, int idPoc, DateTime fecha);

        Task<ReporteIngresoTe?> ObtenerReporteTeOtros(int idPoc, DateTime fecha, TransitoEspecial transitoEspecial);

        Task<ReporteVerifMensual?> ObtenerReporteVerifMensual(int idNomina, int idPoc, DateTime fecha);
    }
}