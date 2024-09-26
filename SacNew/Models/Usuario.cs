using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contrasena { get; set; }
        public string? NombreCompleto { get; set; }
        public bool Activo { get; set; }
    }
}
