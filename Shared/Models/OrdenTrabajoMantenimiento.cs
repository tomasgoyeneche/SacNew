using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class OrdenTrabajoMantenimiento
    {
        public int IdOrdenTrabajoMantenimiento { get; set; }

        public int IdOrdenTrabajo { get; set; }
        public int? IdMantenimiento { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public int? IdTipoMantenimiento { get; set; }
        public string AplicaA { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public decimal? ManoObra { get; set; }
        public decimal? Horas { get; set; }
        public decimal? PrecioRepuestos { get; set; }

        public bool Activo { get; set; } = true;
    }
}
