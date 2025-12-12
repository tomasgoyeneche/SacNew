namespace Shared.Models
{
    public class Articulo
    {
        public int IdArticulo { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public int IdMedida { get; set; }

        public int IdArticuloFamilia { get; set; }

        public int IdPosta { get; set; }

        public int? IdArticuloMarca { get; set; }

        public int? IdArticuloModelo { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal? PedidoMinimo { get; set; }

        public decimal? PedidoMaximo { get; set; }

        public decimal? StockCritico { get; set; }
        public bool Dolar { get; set; } = false;
        public bool Activo { get; set; } = true;
    }
}