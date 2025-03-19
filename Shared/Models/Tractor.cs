using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Tractor
    {
        public int IdTractor { get; set; }

        // Datos de `Tractor`
        public string Patente { get; set; } = string.Empty;
        public DateTime Anio { get; set; }
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public decimal Tara { get; set; }
        public int Hp { get; set; }
        public int Combustible { get; set; }
        public int Cmt { get; set; }
        public int? IdEmpresaSatelital { get; set; } // Puede ser `null` si no existe el registro
        public DateTime FechaAlta { get; set; }

        // Solo para verificación (No se modifica)
        public int IdEmpresa { get; set; }

        public int IdPais { get; set; }

        public string Configuracion { get; set; }

        public bool Activo { get; set; }
    }
}
