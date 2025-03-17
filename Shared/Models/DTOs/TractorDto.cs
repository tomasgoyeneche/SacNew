namespace Shared.Models
{
    public class TractorDto
    {
        public int IdTractor { get; set; }
        public string Patente { get; set; }
        public DateTime? Anio { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }

        public int Tara { get; set; }
        public int Hp { get; set; }
        public int Combustible { get; set; }
        public int Cmt { get; set; }
        public string Configuracion { get; set; }

        public string Satelital_Descripcion { get; set; }
        public string Satelital_usuario { get; set; }
        public string Satelital_clave { get; set; }

        public string Empresa_Nombre { get; set; }
        public string Empresa_Cuit { get; set; }
        public string Empresa_Tipo { get; set; }

        public DateTime? FechaAlta { get; set; }
        public DateTime? Ruta { get; set; }
        public DateTime? Vtv { get; set; }
    }
}