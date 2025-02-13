using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Cia
    {
        public int IdCia { get; set; }
        public string NombreFantasia { get; set; } = string.Empty;
        public int IdTipoCia { get; set; }
        public bool Activo { get; set; }
    }
}
