namespace Shared.Models
{
    public class ConsumoGasoilAutorizadoDto
    {
        public int IdConsumoGasoil { get; set; }
        public string? NumeroPoc { get; set; }
        public string? NumeroVale { get; set; }
        public int IdPrograma { get; set; }
        public decimal LitrosAutorizados { get; set; }
        public decimal LitrosCargados { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaCarga { get; set; }
    }
}