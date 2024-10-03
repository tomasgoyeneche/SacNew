namespace SacNew.Models
{
    public class POC
    {
        public int IdPOC { get; set; }
        public string NumeroPOC { get; set; }
        public int IdPosta { get; set; }
        public int IdNomina { get; set; }
        public decimal Odometro { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuario { get; set; }
        public bool Activo { get; set; }
    }
}