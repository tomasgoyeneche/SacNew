namespace Shared.Models
{
    public class EmpresaSatelital
    {
        public int IdEmpresaSatelital { get; set; }  // Identificador único
        public int IdEmpresa { get; set; }          // Relación con la empresa
        public int IdSatelital { get; set; }        // Relación con la tabla Satelital
        public string? Usuario { get; set; }         // Usuario para el acceso
        public string? Clave { get; set; }           // Clave para el acceso
        public bool Activo { get; set; }            // Indica si está activo
    }
}