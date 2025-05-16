using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class GuardiaIngreso
    {
        public int IdGuardiaIngreso { get; set; }

        public int IdPosta { get; set; } = 0;
        public int TipoIngreso { get; set; } = 0;

        public int IdNomina { get; set; }
        public int IdTe { get; set; }
        public int IdGuardiaIngresoOtros { get; set; }

        public int IdGuardiaEstado { get; set; } = 0;
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;
    }
}
