using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class VehiculoModelo
    {
        public int IdModelo { get; set; }
        public string NombreModelo { get; set; } = string.Empty;
        public int IdMarca { get; set; }
    }
}
