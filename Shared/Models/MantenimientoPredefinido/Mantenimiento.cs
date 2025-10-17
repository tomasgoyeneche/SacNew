using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Mantenimiento
    {
        public int IdMantenimiento { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int IdTipoMantenimiento { get; set; }
        public string AplicaA { get; set; } = string.Empty; // "Tractor", "Semi" o "Unidad"
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public int? KilometrosIntervalo { get; set; }
        public int? DiasIntervalo { get; set; }
    }
}
