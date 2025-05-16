namespace Shared.Models
{
    public class EmpresaSatelitalDto
    {
        public int idEmpresaSatelital { get; set; }
        public string? Descripcion { get; set; }  // Descripción del satelital
        public string? Usuario { get; set; }      // Usuario de acceso
        public string? Clave { get; set; }        // Clave de acceso
    }
}