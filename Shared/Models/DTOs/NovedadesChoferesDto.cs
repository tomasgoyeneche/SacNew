using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class NovedadesChoferesDto
    {
        public int idEstadoChofer { get; set; } 

        public int idChofer { get; set; }   

        public string NombreCompleto { get; set; } = string.Empty;  

        public int idEstado { get; set; }   

        public string Descripcion { get; set; } = string.Empty; 

        public string Abreviado { get; set; } = string.Empty;

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int Dia { get; set; } = 0;   

        public string Disponible { get; set; } = string.Empty;   

        public string Observaciones { get; set; } = string.Empty;   

    }
}
