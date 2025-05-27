namespace Shared.Models.DTOs.Reports
{
    public class PlanillaNominaConsumosDto
    {
        public string? Empresa { get; set; }

        // Tractor
        public string? PatenteTractor { get; set; }

        public DateTime? VTVTractor { get; set; }
        public DateTime? SeguroTractor { get; set; }

        // Semi
        public string? PatenteSemi { get; set; }

        public DateTime? VTV { get; set; }
        public DateTime? SeguroSemi { get; set; }
        public DateTime? Estanqueidad { get; set; }
        public DateTime? VisualInterna { get; set; }
        public DateTime? VisualExterna { get; set; }
        public DateTime? Espesor { get; set; }

        // Unidad
        public DateTime? Checklist { get; set; }

        public DateTime? Calibrado { get; set; } //VerificacionMensual
        public DateTime? MasYPF { get; set; }
        public DateTime? SeguroUnidad { get; set; }

        // Chofer
        public string? Chofer { get; set; }

        public DateTime? Licencia { get; set; }
        public DateTime? PsicofisicoApto { get; set; }
        public DateTime? PsicofisicoCurso { get; set; }
        public DateTime? SeguroDeVida { get; set; }
        public DateTime? Art { get; set; }
    }
}