namespace Shared.Models
{
    public class Disponible
    {
        public int IdDisponible { get; set; }
        public int IdNomina { get; set; }
        public DateTime FechaDisponible { get; set; }
        public int IdOrigen { get; set; }
        public int? IdDestino { get; set; }
        public int Cupo { get; set; }
        public string Observaciones { get; set; }
        public int? IdDisponibleEstado { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}