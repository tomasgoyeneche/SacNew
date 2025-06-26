namespace Shared.Models
{
    public class Programa
    {
        public int IdPrograma { get; set; }
        public int IdDisponible { get; set; }
        public int? IdPedido { get; set; }
        public int IdOrigen { get; set; }
        public int IdProducto { get; set; }
        public int Cupo { get; set; }
        public int AlbaranDespacho { get; set; }
        public int PedidoOr { get; set; }
        public DateTime? FechaCarga { get; set; }
        public DateTime? FechaEntrega { get; set; }

        public decimal? CargaLlegadaOdometer { get; set; }
        public DateTime? CargaLlegada { get; set; }
        public decimal? CargaIngresoOdometer { get; set; }
        public DateTime? CargaIngreso { get; set; }
        public decimal? CargaSalidaOdometer { get; set; }
        public DateTime? CargaSalida { get; set; }

        public int? CargaRemito { get; set; }
        public string? CargaRemitoRuta { get; set; }
        public DateTime? CargaRemitoFecha { get; set; }
        public int? CargaRemitoUnidad { get; set; }
        public int? CargaRemitoKg { get; set; }
        public string? CargaCheck { get; set; }

        public decimal? EntregaLlegadaOdometer { get; set; }
        public DateTime? EntregaLlegada { get; set; }
        public decimal? EntregaIngresoOdometer { get; set; }
        public DateTime? EntregaIngreso { get; set; }
        public decimal? EntregaSalidaOdometer { get; set; }
        public DateTime? EntregaSalida { get; set; }

        public int? EntregaRemito { get; set; }
        public string? EntregaRemitoRuta { get; set; }
        public DateTime? EntregaRemitoFecha { get; set; }
        public int? EntregoRemitoUnidad { get; set; }
        public int? EntregaRemitoKg { get; set; }
        public string? EntregaCheck { get; set; }

        public string? Observaciones { get; set; }
        public int? IdProgramaEstado { get; set; }
    }
}