namespace Shared.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int? AlbaranDespacho { get; set; } = 0;
        public int? PedidoOr { get; set; } = 0;
        public int IdLocacion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaCarga { get; set; }
        public int Cantidad { get; set; }
        public int IdChofer { get; set; }
        public int IdUnidad { get; set; }
        public string? Observaciones { get; set; } = string.Empty;
        public int IdUsuario { get; set; } = 0;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
    }
}