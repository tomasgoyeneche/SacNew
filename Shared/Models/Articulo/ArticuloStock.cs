namespace Shared.Models
{
    public class ArticuloStock
    {
        public int IdArticuloStock { get; set; }
        public int IdArticulo { get; set; }
        public int IdPosta { get; set; }
        public decimal? CantidadActual { get; set; }
        public bool Activo { get; set; } = true;
    }
}