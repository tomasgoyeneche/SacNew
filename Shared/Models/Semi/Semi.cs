namespace Shared.Models
{
    public class Semi
    {
        public int IdSemi { get; set; }

        // Datos de `Semi`
        public string Patente { get; set; } = string.Empty;

        public DateTime Anio { get; set; }
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public decimal Tara { get; set; }

        public string Configuracion { get; set; }

        public int IdEmpresa { get; set; }

        public DateTime FechaAlta { get; set; }

        // Datos de `SemiCisterna`
        public decimal LitroNominal { get; set; }

        public decimal Cubicacion { get; set; }
        public decimal Espesor { get; set; }
        public int IdTipoCarga { get; set; }
        public int Compartimientos { get; set; }
        public int IdMaterial { get; set; }
        public bool CertificadoCompatibilidad { get; set; }
        public string Inv { get; set; }
        public bool Activo { get; set; }
    }
}