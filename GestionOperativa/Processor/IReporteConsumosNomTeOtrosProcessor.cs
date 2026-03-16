using GestionDocumental.Reports;
using Shared.Models;

namespace GestionOperativa.Processor
{
    public interface IReporteConsumosNomTeOtrosProcessor
    {
        Task<ReporteControlOperativoConsumos?> ObtenerReporteConsumosNomina(int idNomina, int idPoc, DateTime fecha, int nroControl);

        Task<ReporteIngresoTe?> ObtenerReporteTeOtros(int idPoc, DateTime fecha, TransitoEspecial transitoEspecial, int nroControl);

        Task<ReporteVerifMensual?> ObtenerReporteVerifMensual(int idNomina, int idPoc, DateTime fecha);
    }
}