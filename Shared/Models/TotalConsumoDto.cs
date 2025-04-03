using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class TotalConsumoDto
    {
        public string Concepto { get; set; }

        public decimal PrecioTotal { get; set; }

        public decimal Cantidad { get; set; }

        public int Tickets { get; set; }
    }
}
