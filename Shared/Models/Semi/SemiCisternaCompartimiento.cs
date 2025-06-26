using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class SemiCisternaCompartimiento
    {
        public int IdCompartimiento { get; set; }
        public int IdSemi { get; set; }
        public int NumeroCompartimiento { get; set; }
        public decimal CapacidadLitros { get; set; }
        public bool Activo { get; set; }
    }
}
