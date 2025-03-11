using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class SemiDto
    {
        public int IdSemi { get; set; }
        public string Patente { get; set; }
        public DateTime? Anio { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }

        public int Tara { get; set; }
        public int LitroNominal { get; set; }

        public int LitrosTotal { get; set; }
        public string LitrosDetalle { get; set; }
        public int Compartimientos { get; set; }

        public bool CertificadoCompatibilidad { get; set; }
        public int Cubicacion { get; set; }
        public string Tipo_Carga { get; set; }
        public string Configuracion { get; set; }
        public string Material { get; set; }
        
        public int Espesor { get; set; }


        public string Satelital_Descripcion { get; set; }
        public string Satelital_usuario { get; set; }
        public string Satelital_clave { get; set; }

        public string Empresa_Nombre { get; set; }
        public string Empresa_Cuit { get; set; }
        public string Empresa_Tipo { get; set; }

        public DateTime? FechaAlta { get; set; }

        public int Inv { get; set;  }
        public DateTime? Ruta { get; set; }
        public DateTime? Vtv { get; set; }
        public DateTime? CisternaEspesor { get; set; }
        public DateTime? VisualInterna { get; set; }
        public DateTime? VisualExterna { get; set; }
        public DateTime? Estanqueidad { get; set; }

    }
}
