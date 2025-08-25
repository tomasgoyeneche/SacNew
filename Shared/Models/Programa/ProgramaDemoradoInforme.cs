namespace Shared.Models
{
    public class ProgramaDemoradoInforme
    {
        public string? Producto { get; set; }
        public int TD { get; set; }
        public int Pedido { get; set; }
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

        public DateTime? HorarioPresentacion { get; set; }
        public string? Motivo { get; set; } = string.Empty;
        public string? Origen { get; set; } = string.Empty;



    }
}