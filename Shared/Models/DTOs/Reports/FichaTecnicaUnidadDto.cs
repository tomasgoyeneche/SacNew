namespace Shared.Models.DTOs
{
    public class FichaTecnicaUnidadDto
    {
        // Unidad
        public string? EmpresaUnidad { get; set; }

        public string? CuitUnidad { get; set; }
        public string? TipoEmpresa { get; set; }
        public decimal TaraTotal { get; set; }
        public DateTime? Calibrado { get; set; }
        public DateTime? Checklist { get; set; }
        public DateTime? MasYPF { get; set; }

        // Tractor
        public string? PatenteTractor { get; set; }

        public DateTime? AnioTractor { get; set; }
        public string? MarcaTractor { get; set; }
        public string? ModeloTractor { get; set; }
        public int TaraTractor { get; set; }
        public int Hp { get; set; }
        public int Combustible { get; set; }
        public int Cmt { get; set; }
        public string? EmpresaTractor { get; set; }
        public string? CuitTractor { get; set; }
        public string? TipoEmpresaTractor { get; set; }
        public string? ConfiguracionTractor { get; set; }
        public DateTime? FechaAltaTractor { get; set; }
        public DateTime? VtvTractor { get; set; }

        // Semi
        public string? PatenteSemi { get; set; }

        public DateTime? AnioSemi { get; set; }
        public string? MarcaSemi { get; set; }
        public string? ModeloSemi { get; set; }
        public int TaraSemi { get; set; }
        public string? ConfiguracionSemi { get; set; }
        public string? EmpresaSemi { get; set; }
        public string? CuitSemi { get; set; }
        public string? TipoEmpresaSemi { get; set; }
        public DateTime? FechaAltaSemi { get; set; }
        public string? TipoCarga { get; set; }
        public int Compartimientos { get; set; }
        public decimal LitroNominal { get; set; }
        public decimal Cubicacion { get; set; }
        public string? Material { get; set; }
        public bool CertificadoCompatibilidad { get; set; }
        public int Inv { get; set; }
        public DateTime? VTVCisterna { get; set; }
        public DateTime? CisternaEspesor { get; set; }
        public DateTime? VisualInterna { get; set; }
        public DateTime? VisualExterna { get; set; }
        public DateTime? Estanqueidad { get; set; }

        // Imágenes
        public string? RutaFotoNomina { get; set; }

        public string? RutaConfiguracionTractor { get; set; }
        public string? RutaConfiguracionSemi { get; set; }

        public string? SeguroTractor { get; set; }
        public string? SeguroSemi { get; set; }
    }
}