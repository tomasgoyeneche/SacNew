using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class MovimientoComprobanteDto
    {
        public int IdMovimientoComprobante { get; set; }
        public int IdMovimientoStock { get; set; }
        public string TipoComprobanteNombre { get; set; } = string.Empty;
        public string NroComprobante { get; set; } = string.Empty;
        public string ProveedorNombre { get; set; } = string.Empty;
        public string RutaComprobante { get; set; } = string.Empty;
    }
}
