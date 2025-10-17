using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class MovimientoComprobante
    {
        public string Nombre { get; set; } = string.Empty;
        public int IdMovimientoComprobante { get; set; }
        public int IdMovimientoStock { get; set; }
        public int IdTipoComprobante { get; set; }
        public string NroComprobante { get; set; } = string.Empty;
        public int IdProveedor { get; set; }
        public bool Activo { get; set; }
        public string RutaComprobante { get; set; } = string.Empty;
    }
}
