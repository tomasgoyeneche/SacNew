namespace Shared.Models
{
    public class ImportVolvoConnect
    {
        public int IdImportVolvoConnect { get; set; }
        public int IdUnidad { get; set; }
        public decimal Kilometros { get; set; }
        public decimal PromedioGasoilEnMarcha { get; set; }
        public decimal GasoilEnMarcha { get; set; }
        public decimal PromedioGasoilEnConduccion { get; set; }
        public decimal GasoilEnConduccion { get; set; }
        public int IdPeriodo { get; set; }
    }
}