using Core.Reports;
using GestionDocumental.Reports;
using GestionOperativa.Reports;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Processor
{
    public interface IReporteNominasProcessor
    {
        Task<ReporteNominaActual?> ObtenerReporteNominaActual();

        Task<ReporteNominaMetanolActiva?> ObtenerReporteNominaMetanol();
    }
}
