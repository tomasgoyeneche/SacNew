using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class HistorialUnidadDto
    {
        public string Empresa { get; set; }
        public string Tractor { get; set; }
        public string Semi { get; set; }
        public string Estado { get; set; }
        public DateTime EstadoFecha { get; set; }
        public string EstadoActual { get; set; }
    }
}
