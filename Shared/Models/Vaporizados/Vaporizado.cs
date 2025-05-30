using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Vaporizado
    {
        public int IdVaporizado { get; set; }
        public string? NroCertificado { get; set; } = string.Empty;
        public string? RemitoDanes { get; set; } = string.Empty;
        public int IdPosta { get; set; }
        public int? CantidadCisternas { get; set; }
        public int? IdVaporizadoMotivo { get; set; }

        public DateTime? FechaInicio { get; set; } = DateTime.Now;
        public DateTime? FechaFin { get; set; } = DateTime.Now;


        public int? IdVaporizadoZona { get; set; }
        public int TipoIngreso { get; set; } = 1; 
        public bool EsExterno { get; set; } = false; // Indica si el vaporizado es externo (true) o interno (false)
        public int? IdNomina { get; set; } = 0; // Identificador de la nómina asociada al vaporizado, si aplica
        public int? IdTe { get; set; } = 0; // Identificador de la nómina asociada al vaporizado, si aplica


        public string? Observaciones { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;

    }
}
