using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class RuteoResumen
    {
        public string Estado { get; set; } = string.Empty;
        public int Cantidad { get; set; } = 0;  
        public int Orden { get; set; } = 0; // Para ordenar los estados en el grid  
    }
}
