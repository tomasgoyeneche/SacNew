namespace Shared.Models
{
    public class VehiculoMarca
    {
        public int IdMarca { get; set; }
        public string NombreMarca { get; set; } = string.Empty;
        public int IdTipoVehiculo { get; set; } // 🔹 1 = Tractor, 2 = Semirremolque
    }
}