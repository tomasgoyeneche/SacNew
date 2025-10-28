using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class OrdenTrabajoTarea
    {
        public int IdOrdenTrabajoTarea { get; set; }
        public int IdOrdenTrabajoMantenimiento { get; set; }

        public int? IdTarea { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public decimal? ManoObra { get; set; }
        public decimal? Horas { get; set; }
        public decimal? TotalEstimado { get; set; }

        public bool Activo { get; set; } = true;
    }
}
