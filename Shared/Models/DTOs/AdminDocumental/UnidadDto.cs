namespace Shared.Models
{
    public class UnidadDto
    {
        public int IdUnidad { get; set; }
        public string? Tractor_Patente { get; set; }
        public string? Tractor_Configuracion { get; set; }
        public string? Empresa_Tractor { get; set; }
        public string? Cuit_Tractor { get; set; }

        public string? Semirremolque_Patente { get; set; }
        public string? Semirremolque_Configuracion { get; set; }
        public string? Empresa_Semi { get; set; }
        public string? Cuit_Semi { get; set; }

        public string? Empresa_Unidad { get; set; }
        public string? Cuit_Unidad { get; set; }
        public string? Tipo_Empresa { get; set; }

        public decimal TaraTotal { get; set; }

        public string? Trafico_Nombre { get; set; }
        public DateTime? VerifMensual { get; set; }
        public DateTime? Checklist { get; set; }
        public DateTime? MasYPF { get; set; }

        public string PatenteCompleta => $"{Tractor_Patente} - {Semirremolque_Patente}";
    }
}