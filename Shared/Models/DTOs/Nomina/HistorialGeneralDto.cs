using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class HistorialGeneralDto
    {
        public int IdNomina { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}