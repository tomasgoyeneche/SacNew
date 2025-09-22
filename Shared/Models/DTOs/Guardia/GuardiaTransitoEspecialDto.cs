using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class GuardiaTransitoEspecialDto
    {
        public string Zona { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;
        public string Chofer { get; set; } = string.Empty;
        public int Dni { get; set; }

        public DateTime? Licencia { get; set; }
        public DateTime? Art { get; set; }

        public DateTime? Seguro { get; set; }
        public DateTime? Ingreso { get; set; }
        public DateTime? Salida { get; set; }


    }
}
