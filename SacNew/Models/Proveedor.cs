using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Models
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Codigo { get; set; }
        public string RazonSocial { get; set; }
        public int NumeroFicha { get; set; }
        public bool Activo { get; set; }
    }
}
