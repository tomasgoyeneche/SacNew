namespace Shared.Models
{
    public class VistaProgramaGridDto
    {
        public int Id { get; set; }
        public string Tractor { get; set; }
        public string Chofer { get; set; }
        public string Empresa { get; set; }
        public int AlbaranDespacho { get; set; }
        public string Producto { get; set; }
        public string Origen { get; set; }
        public DateTime Carga { get; set; }
        public string RtoC { get; set; }
        public string RtoCD { get; set; }
        public string Destino { get; set; }
        public DateTime Entrega { get; set; }
        public string RtoE { get; set; }
        public string RtoED { get; set; }
        public string TotalProd { get; set; }
        public string HoraCarga { get; set; }
        public string HoraEnViaje { get; set; }
        public string HoraEntrega { get; set; }
        public string Recorrido { get; set; }
    }
}