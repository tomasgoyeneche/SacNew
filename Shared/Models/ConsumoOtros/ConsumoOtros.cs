namespace Shared.Models
{
    public class ConsumoOtros
    {
        public int IdConsumoOtros { get; set; }
        public int IdPOC { get; set; }
        public int IdConsumo { get; set; }
        public string? NumeroVale { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal ImporteTotal { get; set; }
        public string? Aclaracion { get; set; }
        public DateTime FechaRemito { get; set; }
        public bool Activo { get; set; } = true;
        public bool Dolar { get; set; }

        public decimal PrecioDolar { get; set; }
    }
}