namespace Shared.Models
{
    public class ProgramaDemoradoInforme
    {
        public string? Producto { get; set; }
        public int Albaran { get; set; }
        public int PedidoOr { get; set; }
        public string? Destino { get; set; }

        public DateTime FechaEntrega { get; set; }
        public DateTime FechaCarga { get; set; }

        public int Capacidad { get; set; }
        public string Transporte { get; set; }
        public int CodTransporte { get; set; }
        public string Dni { get; set; }
        public string Chofer { get; set; }
        public string Tractor { get; set; }
        public string Semi { get; set; }
        public string Observaciones { get; set; }

        public string? HorarioPresentacion { get; set; } = string.Empty;

    }
}