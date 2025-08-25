namespace Shared.Models
{
    public class PedidoDto
    {
        public int IdPedido { get; set; }
        public string Producto { get; set; } = string.Empty;
        public string AlbaranDespacho { get; set; } = string.Empty;
        public string PedidoOr { get; set; } = string.Empty;
        public string Locacion { get; set; } = string.Empty;
        public DateTime FechaCarga { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int CantidadPedido { get; set; }
        public string Chofer { get; set; } = string.Empty;
        public string Tractor { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
    }
}