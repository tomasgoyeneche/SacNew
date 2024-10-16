namespace SacNew.Models
{
    public class Nomina
    {
        public int IdNomina { get; set; }
        public string PatenteTractor { get; set; }
        public string PatenteSemi { get; set; }
        public string NombreChofer { get; set; }

        public string ApellidoChofer { get; set; }

        public int idEmpresa { get; set; }

        public string DescripcionNomina
        {
            get
            {
                return $"{PatenteTractor} - {PatenteSemi} - {NombreChofer} {ApellidoChofer}";
            }
        }
    }
}