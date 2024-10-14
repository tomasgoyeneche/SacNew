namespace SacNew.Models
{
    public class Posta
    {
        public int IdPosta { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string? Direccion { get; set; }
        public int IdProvincia { get; set; }
    }
}