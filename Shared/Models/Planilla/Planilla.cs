using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Planilla
    {
        public int IdPlanilla { get; set; } 

        public string Titulo { get; set; }  

        public string Pie { get; set; }

        public DateTime Fecha { get; set; } 

        public bool Relevable { get; set; }

        public bool Activo { get; set; }
    }
}
