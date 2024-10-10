namespace SacNew.Models
{
    public class LocacionKilometrosEntre
    {
        public int IdKilometros { get; set; }
        public int IdLocacionOrigen { get; set; }
        public int IdLocacionDestino { get; set; }
        public int Kilometros { get; set; }

        // Relación con locaciones
        public Locacion LocacionDestino { get; set; }  // Locacion destino
    }
}