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
        public DateTime Licencia { get; set; }
        public DateTime ExamenAnual { get; set; }
        public DateTime PsicofisicoApto { get; set; }
        public DateTime PsicofisicoCurso { get; set; }
    }
}