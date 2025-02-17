namespace Shared.Models
{
    public class ConsumoGasoil
    {
        public int IdConsumoGasoil { get; set; } // Identificador único
        public int IdPOC { get; set; } // Relación con la POC
        public int IdConsumo { get; set; } // Relación con el concepto de gasoil
        public string? NumeroVale { get; set; } // Número del vale o ticket de carga
        public int IdPrograma { get; set; } // Nuevo campo

        public decimal LitrosAutorizados { get; set; } // Litros autorizados (nullable)
        public decimal LitrosCargados { get; set; } // Litros efectivamente cargados
        public decimal PrecioTotal { get; set; } // Precio total (nullable)
        public string? Observaciones { get; set; } // Observaciones adicionales
        public bool Activo { get; set; } = true; // Estado del consumo (por defecto activo)
        public DateTime FechaCarga { get; set; } // Fecha en que se cargó el combustible
    }
}