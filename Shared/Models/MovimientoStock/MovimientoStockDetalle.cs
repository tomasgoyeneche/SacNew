using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class MovimientoStockDetalle
    {
        public int IdMovimientoDetalle { get; set; }
        public int IdMovimientoStock { get; set; }
        public int IdArticulo { get; set; }
        public int IdPosta { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public bool Activo { get; set; }
    }
}
