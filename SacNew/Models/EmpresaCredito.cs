using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Models
{
    public class EmpresaCredito
    {
        public int IdCredito { get; set; }
        public int IdEmpresa { get; set; }
        public string Mes { get; set; } 
        public decimal CreditoAsignado { get; set; }
        public decimal CreditoConsumido { get; set; }
        public decimal CreditoDisponible { get; set; }  
        public bool Activo { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
