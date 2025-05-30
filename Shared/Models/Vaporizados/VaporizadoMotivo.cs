using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class VaporizadoMotivo
    {
        public int IdVaporizadoMotivo { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;
    }
}
