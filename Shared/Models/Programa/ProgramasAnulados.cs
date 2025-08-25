using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ProgramasAnulados
    {
        public int IdPrograma { get; set; }          // Identificador único del programa    
        public DateTime Disponible { get; set; }    // Fecha y hora en que el programa está disponible
        public string Empresa { get; set; } = string.Empty; // Nombre de la empresa asociada al programa
        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;
        public string Chofer { get; set; } = string.Empty;

        public string Origen { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
    }
}
