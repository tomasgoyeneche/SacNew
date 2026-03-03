using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class UnidadChoferSchedulerDto
    {
        public int IdEstadoChoferUnidad { get; set; }
        public int IdChoferUnidad { get; set; }

        public DateTime Inicio { get; set; }          // FechaInicio
        public DateTime FinExclusivo { get; set; }    // FechaFin + 1 (NO inclusivo)

        public string Abreviado { get; set; } = string.Empty;
        public string DescripcionEstado { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;

        // opcional: para colorear o lógica extra
        public int IdEstado { get; set; }
        public bool Disponible { get; set; }
    }
}
