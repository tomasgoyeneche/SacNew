using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal? ManoObra { get; set; }
        public decimal? Horas { get; set; }
        public bool Activo { get; set; } = true;
    }
}
