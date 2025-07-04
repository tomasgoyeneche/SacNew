using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class TransoftMetanol
    {
        public int Id { get; set; }
        public string? Origen { get; set; } 
        public DateTime Fecha { get; set; }
        public string? Remito { get; set; }
        public string? Tractor { get; set; }
        public string? Semi { get; set; }
        public string? Empresa { get; set; }
        public string? Albaran { get; set; }
        public int? Kg { get; set; }
        public string? Destino { get; set; }
        public string? Chofer { get; set; }
        public int? Dni { get; set; }
        public int? Cuit { get; set; }
        public string? Producto { get; set; }

    }
}
