using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class VistaPrograma
    {
        public int IdPrograma { get; set; }
        public int IdNomina { get; set; }
        public int Cliente { get; set; }

        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Chofer { get; set; } = string.Empty;

        public int AlbaranDespacho { get; set; }
        public int PedidoOr { get; set; }
        public string Producto { get; set; } = string.Empty;
        public int IdOrigen { get; set; }
        public string Origen { get; set; } = string.Empty;
        public DateTime CargaIngreso { get; set; }
        public DateTime CargaSalida { get; set; }
        public int? CargaRemito { get; set; }
        public DateTime? CargaRemitoFecha { get; set; }
        public int? CargaUnidad { get; set; }
        public int? CargaRemitoKg { get; set; }
        public int IdDestino { get; set; }
        public string Destino { get; set; } = string.Empty;
        public DateTime EntregaLlegada { get; set; }
        public DateTime EntregaIngreso { get; set; }
        public DateTime EntregaSalida { get; set; }
        public int? EntregaRemito { get; set; }
        public DateTime? EntregaRemitoFecha { get; set; }
        public int? EntregaUnidad { get; set; }
        public int? EntregaRemitoKg { get; set; }
    
        public int MinCarga { get; set; }
        public int MinEnViaje { get; set; }
        public int MinEntrega { get; set; }
        public int MinTotales { get; set; }
        public int KmTotales { get; set; }
        public string HoraCarga { get; set; } = string.Empty;
        public string HoraEnViaje { get; set; } = string.Empty;
        public string HoraEntrega { get; set; } = string.Empty;
        public string HoraTotal { get; set; } = string.Empty;
        public int Estadia { get; set; }
        public DateTime? FechaPrograma { get; set; }
        public int EstadiaCarga { get; set; }

    }
}
