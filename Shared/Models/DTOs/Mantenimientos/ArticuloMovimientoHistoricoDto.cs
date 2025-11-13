using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ArticuloMovimientoHistoricoDto
    {
        public int IdArticulo { get; set; }
        public int IdPosta { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public string CodigoArticulo { get; set; } = string.Empty;
        public string DescripcionArticulo { get; set; } = string.Empty;
        public DateTime FechaMovimiento { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public string Familia { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public string PostaDescripcion { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
    }
}
