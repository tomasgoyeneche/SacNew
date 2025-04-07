namespace Shared.Models
{
    public class InformeConsumoPocDto
    {
        public int IdRegistro { get; set; }
        public string Tipo_Consumo { get; set; }
        public int IdPoc { get; set; }
        public string NumeroPoc { get; set; }
        public int IdPosta { get; set; }
        public string Codigo_Posta { get; set; }
        public int IdUnidad { get; set; }
        public int IdEmpresa { get; set; }
        public int IdChofer { get; set; }

        public string Tractor_Patente { get; set; }
        public string Semi_Patente { get; set; }
        public string Empresa_Nombre { get; set; }
        public string Chofer_Nombre { get; set; }

        public decimal Odometro { get; set; }
        public string Comentario { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaCierre { get; set; }

        public int IdPeriodo { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }

        public int IdConsumo { get; set; }
        public string Concepto_Codigo { get; set; }
        public string NumeroVale { get; set; }

        public decimal LitrosAutorizados { get; set; }
        public decimal LitrosCargados { get; set; }
        public decimal PrecioTotal { get; set; }

        public string Observaciones { get; set; }
        public bool Dolar { get; set; }
        public DateTime FechaCarga { get; set; }
    }
}