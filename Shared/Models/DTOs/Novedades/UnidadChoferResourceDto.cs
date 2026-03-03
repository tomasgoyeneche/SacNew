using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class UnidadChoferResourceDto
    {
        public int IdEntidad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Alta { get; set; }
        public DateTime? Baja { get; set; }
    }
}
