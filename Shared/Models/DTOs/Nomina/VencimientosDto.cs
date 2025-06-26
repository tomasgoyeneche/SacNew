namespace Shared.Models
{
    public class VencimientosDto
    {
        public int Entidad { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaVencimiento { get; set; } = DateTime.Now;
    }
}