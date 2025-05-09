using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ChoferEstado
    {
        public int IdEstadoChofer { get; set; } 

        public int IdChofer { get; set; }

        public int IdEstado { get; set; }

        public string Observaciones { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public bool Disponible { get; set; }    
        public bool Activo { get; set; }    

    }
}
