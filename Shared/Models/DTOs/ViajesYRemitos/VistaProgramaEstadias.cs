using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class VistaProgramaEstadias
    {
        public int IdPrograma { get; set; }

        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Chofer { get; set; } = string.Empty;
        public DateTime? FechaPrograma { get; set; }
        public int AlbaranDespacho { get; set; }
        public int PedidoOr { get; set; }
        public string Producto { get; set; } = string.Empty;
        public string Origen { get; set; } = string.Empty;
        public DateTime CargaIngreso { get; set; }
        public DateTime CargaSalida { get; set; }
        public int EstadiaCarga { get; set; }
        public int? CargaRemito { get; set; }
        public DateTime? CargaRemitoFecha { get; set; }
        public string? CargaUnidad { get; set; }
        public int? CargaRemitoKg { get; set; }
        public string Destino { get; set; } = string.Empty;
        public DateTime EntregaLlegada { get; set; }
        public DateTime EntregaIngreso { get; set; }
        public DateTime EntregaSalida { get; set; }
        public int Estadia { get; set; }
        public int? EntregaRemito { get; set; }
        public DateTime? EntregaRemitoFecha { get; set; }

        public string? EntregaUnidad { get; set; }
        public int? EntregaRemitoKg { get; set; }


        public string HoraCarga { get; set; } = string.Empty;
        public string HoraEnViaje { get; set; } = string.Empty;
        public string HoraEntrega { get; set; } = string.Empty;
        public string HoraTotal { get; set; } = string.Empty;
        public int KmTotales { get; set; }


    }
}
