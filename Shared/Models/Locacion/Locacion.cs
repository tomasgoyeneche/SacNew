namespace Shared.Models
{
    public class Locacion
    {
        public int IdLocacion { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public bool Carga { get; set; }  // Campo booleano para carga
        public bool Descarga { get; set; }  // Campo booleano para descarga
        public bool Activo { get; set; }  // Para baja lógica (activo o inactivo)

        public bool Exportacion { get; set; }  // Campo booleano para exportación   


        public string CargaTexto => Carga ? "Sí" : "No";
        public string DescargaTexto => Descarga ? "Sí" : "No";

        //public List<LocacionProducto>? ProductosCarga { get; set; }  // Relación con productos
        //public List<LocacionKilometrosEntre>? EnlacesKilometros { get; set; }  // Relación con distancias
    }
}