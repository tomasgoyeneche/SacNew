namespace Shared.Models
{
    public class TotalConsumoDto
    {
        public string Concepto { get; set; }

        public decimal PrecioTotal { get; set; }

        public decimal Cantidad { get; set; }

        public int Tickets { get; set; }
    }
}