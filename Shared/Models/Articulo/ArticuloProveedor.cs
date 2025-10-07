using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ArticuloProveedor
    {
        public int IdProveedor { get; set; } // Cambiar

        public string RazonSocial { get; set; } = string.Empty;
        public string CUIT { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
