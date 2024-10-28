using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Models
{
    public class ImportConsumoYpfEnRuta
    {
        public int IdImportConsumoYPF { get; set; }
        public DateTime FechaHora { get; set; }
        public string Localidad { get; set; }
        public string Tarjeta { get; set; }
        public int IdChofer { get; set; }
        public int IdUnidad { get; set; }
        public string Remito { get; set; }
        public int IdConsumo { get; set; }
        public decimal Litros { get; set; }
        public decimal ImporteTotalYer { get; set; }
        public decimal ImporteSinImpuestos { get; set; }
        public string Factura { get; set; }
        public int IdPeriodo { get; set; }

        public bool Chequeado { get; set; }
    }
}
