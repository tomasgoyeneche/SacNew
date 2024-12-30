namespace Shared.Models
{
    public class InformeConsumoUnidad
    {
        public string PatenteTractor { get; set; }          // Patente del tractor
        public decimal ConsumoLitrosMes { get; set; }       // Consumo total en litros del mes
        public decimal KilometrosRecorridos { get; set; }   // Kilómetros recorridos en el mes
        public decimal LitrosCienKilometros { get; set; }   // Litros consumidos por cada 100 km
        public decimal LitrosTotalesYPF { get; set; }       // Litros totales consumidos en YPF
        public decimal PorcentajeYPF { get; set; }          // Porcentaje del consumo en YPF
        public decimal LitrosTotalesMercadoVictoria { get; set; }  // Litros totales de Mercado Victoria
        public decimal PorcentajeMercadoVictoria { get; set; }     // Porcentaje del consumo en Mercado Victoria
        public decimal LitrosTotales { get; set; }          // Litros totales consumidos
        public decimal DiferenciaLitros { get; set; }       // Diferencia de litros entre consumos
        public decimal PorcentajeDiferencia { get; set; }   // Porcentaje de diferencia de consumo
        public decimal LitrosConsumidosReales { get; set; } // Litros consumidos reales
        public decimal DiferenciaLitrosTeoricosReales { get; set; } // Diferencia entre litros teóricos y reales
        public decimal PorcentajeDiferenciaLitrosReales { get; set; } // Porcentaje de diferencia en litros reales

        public int idPeriodo { get; set; }
    }
}