using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Models
{
    public class Periodo
    {
        public int IdPeriodo { get; set; }
        public int IdMes { get; set; }
        public int Anio { get; set; }
        public int Quincena { get; set; }
        public string NombrePeriodo { get; set; }
        public bool Activo { get; set; }
    }
}
