namespace Shared.Models
{
    public class ArticuloStockDepositoDto
    {
        public int IdArticuloStock { get; set; }
        public int IdArticulo { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string ArticuloNombre { get; set; } = string.Empty;
        public string ArticuloDescripcion { get; set; } = string.Empty;
        public string FamiliaNombre { get; set; } = string.Empty;
        public string MarcaNombre { get; set; } = string.Empty;
        public string ModeloNombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }

        public int IdPosta { get; set; }
        public string PostaDescripcion { get; set; } = string.Empty;
        public decimal CantidadActual { get; set; }

        public decimal StockCritico { get; set; }
    }
}