namespace Shared.Models
{
    public class Unidad
    {
        public int IdUnidad { get; set; }
        public int IdTractor { get; set; }
        public int IdSemi { get; set; }
        public int TaraTotal { get; set; }
        public int IdEmpresa { get; set; }
        public bool Activo { get; set; }
        public int IdTrafico { get; set; }

        public DateTime AltaUnidad { get; set; }
        public DateTime? BajaUnidad { get; set; }
    }
}