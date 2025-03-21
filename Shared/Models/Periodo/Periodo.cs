namespace Shared.Models
{
    public class Periodo
    {
        public int IdPeriodo { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public int Quincena { get; set; }
        public string? NombrePeriodo { get; set; }
        public bool Activo { get; set; }
    }
}