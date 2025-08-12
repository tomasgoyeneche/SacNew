using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Acumulado
    {
        public int IdDisponible { get; set; }   
        public string Empresa { get; set; } = string.Empty;
        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;

        public DateTime Disponible { get; set; } = DateTime.Now;
        public string DispoOrigen { get; set; } = string.Empty; 
        public int DispoCupo { get; set; } = 0;
        public string DispoDestino { get; set; } = string.Empty;
        public string DispoObs { get; set; } = string.Empty;
        public string DispoEstado { get; set; } = string.Empty;
        public int IdPrograma { get; set; } = 0;
        public string Chofer { get; set; } = string.Empty;
        public string progOrigen { get; set; } = string.Empty;
        public int ProgCupo { get; set; } = 0;
        public string ProgProducto { get; set; } = string.Empty;
        public string ProgDestino { get; set; } = string.Empty;
        public int AlbaranDespacho { get; set; } = 0;
        public int PedidoOr { get; set; } = 0;

        public DateTime? FechaCarga { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? IngresoCarga { get; set; }
        public DateTime? CargaSalida { get; set; }
        public DateTime? EntregaLlegada { get; set; }
        public DateTime? EntregaIngreso { get; set; }
        public DateTime? EntregaSalida{ get; set; }
        public string ProgEstado { get; set; } = string.Empty;
        public int CargaRemito { get; set; } = 0;
        public int CargaKg { get; set; } = 0;
        public string Unidad { get; set; } = string.Empty;  



    }
}
