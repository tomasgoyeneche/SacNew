using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ProgramaTramo
    {
        public int IdTramo { get; set; }
        public int IdPrograma { get; set; }
        public int IdNomina { get; set; }
        public int IdDestino { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
