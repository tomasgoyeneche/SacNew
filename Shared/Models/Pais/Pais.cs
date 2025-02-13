namespace Shared.Models
{
    public class Pais
    {
        public int IdPais { get; set; }          // Identificador único del país
        public string NombrePais { get; set; }   // Nombre del país
        public string CodigoIso { get; set; }    // Código ISO del país         // Indicador de si está activo
    }
}