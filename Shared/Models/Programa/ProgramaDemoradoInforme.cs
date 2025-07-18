namespace Shared.Models
{
    public class ProgramaDemoradoInforme
    {
        public int IdPrograma { get; set; }
        public string? Producto { get; set; }
        public int AlbaranDespacho { get; set; }
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
        public string Obs { get; set; }

        public DateTime EntregaLlegada { get; set; }
        public DateTime EntregaSalida { get; set; }
    }
}