using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ReportVaporizadoResumen
    {
        public string? Empresa { get; set; } = string.Empty;
        public string? Motivo { get; set; } = string.Empty;

        public int Cantidad { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal PorcentajeTotal { get; set; }
    }
}