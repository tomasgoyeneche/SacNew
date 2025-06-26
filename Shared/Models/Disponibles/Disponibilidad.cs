namespace Shared.Models
{
    public class Disponibilidad
    {
        public DateTime DispoFecha { get; set; }
        public int IdNomina { get; set; }
        public string Tractor { get; set; }
        public string Empresa { get; set; }
        public string Chofer { get; set; }
        public string? Producto { get; set; }
        public string? Destino { get; set; }
        public DateTime FechaEntrega { get; set; }

        public string? Location { get; set; }
        public int Sat { get; set; }
        public string? Estado { get; set; }

        public string? DisOrigen { get; set; }
        public int Cupo { get; set; }
        public string? DisDestino { get; set; }

        public char? Tipo { get; set; }
    }
}