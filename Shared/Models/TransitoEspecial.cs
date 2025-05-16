using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class TransitoEspecial
    {
        public int IdTE { get; set; }

        public string RazonSocial { get; set; } = string.Empty; 

        public string Cuit { get; set; } = string.Empty;    

        public string Apellido { get; set; } = string.Empty;    

        public string Nombre { get; set; } = string.Empty;  

        public string Documento { get; set; } = string.Empty;

        public DateTime Licencia { get; set; } = DateTime.Now;

        public DateTime Art { get; set; } = DateTime.Now;

        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;
        public DateTime Seguro { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;    
    }
}
