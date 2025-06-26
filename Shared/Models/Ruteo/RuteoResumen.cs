namespace Shared.Models
{
    public class RuteoResumen
    {
        public string Estado { get; set; } = string.Empty;
        public int Cantidad { get; set; } = 0;
        public int Orden { get; set; } = 0; // Para ordenar los estados en el grid
        public decimal Porcentaje { get; set; } = 0; // Porcentaje del total de ruteos
    }
}