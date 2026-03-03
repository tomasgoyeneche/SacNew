using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class DisponibilidadHistorica
    {
        public int IdDisponibilidadHistorica { get; set; }  
        public DateTime Fecha { get; set; } = DateTime.Now; 

        public int IdNomina { get; set; } = 0; // Identificador de la nómina asociada a la disponibilidad histórica, si aplica
        public int IdProducto { get; set; } = 0; // Identificador del producto asociado a la disponibilidad histórica, si aplica
        public string Estado { get; set; } = string.Empty; // Estado de la disponibilidad histórica (por ejemplo, "Disponible", "No Disponible", etc.)  
        public int IdEstadoReal { get; set; } = 0; // Identificador del estado real asociado a la disponibilidad histórica, si aplica
        public int TipoEstadoReal { get; set; } = 0; // Tipo de estado real asociado a la disponibilidad histórica, si aplica (por ejemplo, 1 para "Disponible", 2 para "No Disponible", etc.)  
        public string Origen { get; set; } = string.Empty; // Origen de la información de la disponibilidad histórica (por ejemplo, "Sistema", "Manual", etc.)  
        public string? Destino { get; set; } = string.Empty; // Destino de la información de la disponibilidad histórica (por ejemplo, "Base de Datos", "Reporte", etc.)
        public DateTime FechaRegistro { get; set; } = DateTime.Now; // Fecha y hora en que se registró la disponibilidad histórica
    }
}
