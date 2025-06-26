namespace Shared.Models
{
    public class Cupeo
    {
        public int IdNomina { get; set; }
        public int? IdDisponible { get; set; }
        public int? IdPrograma { get; set; }
        public int? IdPedido { get; set; }

        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;
        public string RazonSocial { get; set; } = string.Empty;
        public int? IdChofer { get; set; }
        public string Chofer { get; set; } = string.Empty;
        public int? IdOrigen { get; set; }
        public string Origen { get; set; } = string.Empty;
        public DateTime FechaCarga { get; set; }
        public int? IdDestino { get; set; }
        public string? Destino { get; set; } = string.Empty;
        public DateTime? FechaEntrega { get; set; }
        public int? Cupo { get; set; }
        public string? AlbaranDespacho { get; set; } = string.Empty;
        public string? PedidoOr { get; set; } = string.Empty;
        public int? IdProducto { get; set; }
        public string? Producto { get; set; } = string.Empty;
        public string? Observaciones { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public int? Sat { get; set; }
        public string? Confirmado { get; set; } = string.Empty;
    }
}