using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class OrdenTrabajoDto
    {
         public int IdOrdenTrabajo { get; set; }
        public DateTime FechaEmision { get; set; } = DateTime.Now;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public int? IdNomina { get; set; }
        public decimal? OdometroIngreso { get; set; }
        public decimal? OdometroSalida { get; set; }

        public decimal? HorasEstimadas { get; set; }
        public decimal? CostoEstimado { get; set; }

        /// <summary>
        /// 0 = Creada, 1 = Autorizada, 2 = En Taller, 3 = Finalizada
        /// </summary>
        public byte Fase { get; set; } = 0;
        public string FaseNombre { get; set; } = string.Empty;

        public string PatenteTractor { get; set; } = string.Empty;
        public string PatenteSemi { get; set; } = string.Empty;


        public int? IdLugarReparacion { get; set; }
        public string LugarReparacionNombre { get; set; } = string.Empty;

        public string? Observaciones { get; set; }

        public bool Activo { get; set; } = true;
    }
}
