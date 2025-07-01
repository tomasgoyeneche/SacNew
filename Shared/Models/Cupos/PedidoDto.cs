using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class PedidoDto
    {
        public int IdPedido { get; set; }
        public string Producto { get; set; } = string.Empty;
        public string AlbaranDespacho { get; set; } = string.Empty;
        public string PedidoOr { get; set; } = string.Empty;
        public string Locacion { get; set; } = string.Empty;
        public string FechaCarga { get; set; } = string.Empty;  
        public string FechaEntrega { get; set; } = string.Empty;
        public int CantidadPedido { get; set; }
        public string Chofer { get; set; } = string.Empty;  
        public string Tractor { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
    }
}
