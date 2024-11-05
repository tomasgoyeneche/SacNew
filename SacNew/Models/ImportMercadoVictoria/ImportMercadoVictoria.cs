namespace SacNew.Models
{
    public class ImportMercadoVictoria
    {
        public int IdImportMercadoVictoria { get; set; } // Identificador único
        public int IdUnidad { get; set; }          // Relación con la unidad (tractor + semi)
        public DateTime Fecha { get; set; }        // Fecha del registro
        public decimal Litros { get; set; }        // Cantidad de litros consumidos
        public int IdConsumo { get; set; }         // Relación con el tipo de consumo
        public int IdPeriodo { get; set; }         // Relación con el período
    }
}