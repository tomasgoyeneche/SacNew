namespace Shared.Models
{
    public class Ruteo
    {
        public int IdNomina { get; set; }
        public int IdPrograma { get; set; }

        public string Tractor { get; set; } = string.Empty;
        public string Semi { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;

        public int IdChofer { get; set; }
        public string Chofer { get; set; } = string.Empty;

        public int IdOrigen { get; set; }
        public string Origen { get; set; } = string.Empty;
        public DateTime? FechaCarga { get; set; }
        public int IdDestino { get; set; }
        public string Destino { get; set; } = string.Empty;
        public DateTime? FechaEntrega { get; set; }

        public int Cupo { get; set; } = 0;
        public int AlbaranDespacho { get; set; } = 0;
        public int PedidoOr { get; set; } = 0;
        public int IdProducto { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;

        public DateTime? CargaSalida { get; set; }
        public decimal Odometer { get; set; } = 0.0m;

        public DateTime? EntregaLlegada { get; set; }
        public DateTime? EntregaIngreso { get; set; }

        public string Location { get; set; } = string.Empty;
        public int Sat { get; set; } = 0;

        public string Estado { get; set; } = string.Empty;
    }
}