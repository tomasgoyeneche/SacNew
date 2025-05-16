namespace Shared.Models.DTOs
{
    public class ConsumosUnificadosDto
    {
        public int IdConsumo { get; set; }
        public int IdPoc { get; set; }
        public string? Descripcion { get; set; }

        public string? NumeroVale { get; set; }

        public decimal? LitrosAutorizados { get; set; }

        public decimal? Cantidad { get; set; }

        public decimal? ImporteTotal { get; set; }

        public string? Aclaraciones { get; set; }

        public DateTime FechaRemito { get; set; }
        public int tipoConsumo { get; set; }
    }
}