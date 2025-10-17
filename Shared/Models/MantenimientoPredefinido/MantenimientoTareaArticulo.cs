using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class MantenimientoTareaArticulo
    {
        public int IdMantenimientoTareaArticulo { get; set; }
        public int IdTarea { get; set; }
        public int IdArticulo { get; set; }
        public decimal Cantidad { get; set; }
        public bool Activo { get; set; } = true;
    }
}
