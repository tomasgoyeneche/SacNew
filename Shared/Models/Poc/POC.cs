namespace Shared.Models
{
    public class POC
    {
        public int IdPoc { get; set; }
        public string NumeroPoc { get; set; }
        public int IdPosta { get; set; }
        public int IdUnidad { get; set; }
        public int IdChofer { get; set; }
        public double? Odometro { get; set; }
        public string? Comentario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int IdPeriodo { get; set; }
        public int IdUsuario { get; set; }
        public string Estado { get; set; }
    }
}