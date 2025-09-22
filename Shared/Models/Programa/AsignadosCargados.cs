using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class AsignadosCargados
    {

        public int IdPrograma { get; set; }          // Identificador único del programa    
        public DateTime Disponible { get; set; }    // Fecha y hora en que el programa está disponible
        public string Empresa { get; set; } = string.Empty; // Nombre de la empresa asociada al programa
        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;
        public string Chofer { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;

        public string Origen { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public string AlbaranDespacho { get; set; } = string.Empty;
        public string Producto { get; set; } = string.Empty;

        public DateTime LlegadaCarga { get; set; }    // Fecha y hora en que el programa está disponible
        public DateTime IngresoCarga { get; set; }    // Fecha y hora en que el programa está disponible
        public DateTime SalidaCarga { get; set; }    // Fecha y hora en que el programa está disponible
    }
}
