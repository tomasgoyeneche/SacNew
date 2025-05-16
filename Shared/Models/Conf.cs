namespace Shared.Models
{
    public class Conf
    {
        public int IdConf { get; set; }             // Identificador único
        public string? Ruta { get; set; }           // Ruta del directorio
        public string? Descripcion { get; set; }    // Descripción del propósito de la configuración
        public bool Activo { get; set; }           // Indicador de si está activa
    }
}