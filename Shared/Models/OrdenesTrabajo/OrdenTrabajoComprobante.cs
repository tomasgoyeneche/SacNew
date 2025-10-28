using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class OrdenTrabajoComprobante
    {
        public int IdOrdenTrabajoComprobante { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int IdOrdenTrabajo { get; set; }
        public int? IdTipoComprobante { get; set; }
        public string? NroComprobante { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public string RutaComprobante { get; set; } = string.Empty;
    }
}
