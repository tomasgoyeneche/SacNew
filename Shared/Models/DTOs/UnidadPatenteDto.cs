namespace Shared.Models.DTOs
{
    public class UnidadPatenteDto
    {
        public int IdUnidad { get; set; }
        public string? PatenteTractor { get; set; }
        public string? PatenteSemi { get; set; }

        public int idEmpresa { get; set; }

        public string? DescripcionUnidad
        {
            get
            {
                return $"{PatenteTractor} - {PatenteSemi}";
            }
        }
    }
}