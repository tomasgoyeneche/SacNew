namespace Shared.Models
{
    public class HistorialGeneralDto
    {
        public int IdNomina { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}