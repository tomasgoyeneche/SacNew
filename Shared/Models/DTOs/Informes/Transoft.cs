using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Transoft
    {
        public int Id { get; set; }
        public string? Tractor { get; set; }
        public string? Semi { get; set; }
        public int? Dni { get; set; }
        public string? Origen { get; set; }
        public string? Destino { get; set; }
        public string? Producto { get; set; }
        public DateTime Disponible { get; set; }
        public string? Sinergia { get; set; }

    }
}
