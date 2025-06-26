namespace Shared.Models
{
    public class Nomina
    {
        public int IdNomina { get; set; }

        public int IdUnidad { get; set; } = 0;

        public int IdChofer { get; set; } = 0;

        public string ObsYpf { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; } = DateTime.Now;
        public DateTime FechaBaja { get; set; }
    }
}