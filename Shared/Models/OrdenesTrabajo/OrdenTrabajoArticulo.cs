using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class OrdenTrabajoArticulo
    {
        public int IdOrdenTrabajoArticulo { get; set; }
        public int IdOrdenTrabajoTarea { get; set; }

        public int? IdArticulo { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;

        public decimal PrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public decimal? Estimado { get; set; }

        public bool Activo { get; set; } = true;
        public string Estado { get; set; } = "Pendiente";
    }
}
