using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Unidad
    {
        public int IdUnidad { get;set; }
        public int IdTractor { get; set; }
        public int IdSemi { get; set; }
        public int TaraTotal { get; set; }
        public int IdEmpresa { get; set; }
        public bool Metanol { get; set; }
        public bool LujanCuyo { get; set; }
        public bool Gasoil { get; set; }
        public bool AptoBo { get; set; }
        public bool Activo { get; set; }

    }
}
