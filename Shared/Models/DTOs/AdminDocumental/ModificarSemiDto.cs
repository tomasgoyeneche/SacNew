using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ModificarSemiDto
    {
        public int IdSemi { get; set; }

        // Datos de `Semi`
        public string Patente { get; set; } = string.Empty;
        public DateTime Anio { get; set; }
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public decimal Tara { get; set; }
        public DateTime FechaAlta { get; set; }

        // Datos de `SemiCisterna`
        public int IdTipoCarga { get; set; }
        public int Compartimientos { get; set; }
        public int IdMaterial { get; set; }
    }
}
