using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ChoferDto
    {
        public int IdChofer { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Ubicacion { get; set; }
        public string Domicilio { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Empresa_Nombre { get; set; }
        public string Empresa_Cuit { get; set; }
        public string Empresa_Tipo { get; set; }
        public bool ZonaFria { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool CoberturaCentralizada { get; set; }
        public string Cia_Seguro { get; set; }
        public string Tipo_Cobertura { get; set; }
        public string NumeroPoliza { get; set; }
        public DateTime PagoHasta { get; set; }
        public DateTime VigenciaHasta { get; set; }
        public DateTime Licencia { get; set; }
        public DateTime ExamenAnual { get; set; }
        public DateTime PsicofisicoApto { get; set; }
        public DateTime PsicofisicoCurso { get; set; }
        public DateTime SvoSeguroVida { get; set; }
    }
}
