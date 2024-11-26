namespace SacNew.Models
{
    public class chofer
    {
        public int IdChofer { get; set; }               // Identificador único del chofer
        public string? Apellido { get; set; }            // Apellido del chofer
        public string? Nombre { get; set; }              // Nombre del chofer
        public string? Documento { get; set; }           // Documento del chofer
        public DateTime FechaNacimiento { get; set; }   // Fecha de nacimiento
        public int IdLocalidad { get; set; }            // Relación con la Localidad
        public string? Domicilio { get; set; }           // Dirección del domicilio
        public string? Celular { get; set; }             // Número de celular
        public string? Telefono { get; set; }           // Número de teléfono fijo, opcional
        public int IdEmpresa { get; set; }              // Relación con la Empresa
        public bool ZonaFria { get; set; }              // Indica si pertenece a una zona fría
        public bool Activo { get; set; }                // Estado del chofer (activo/inactivo)
        public DateTime FechaAlta { get; set; }         // Fecha de alta en la empresa

        public string NombreApellido
        {
            get
            {
                return $"{Apellido} - {Nombre}";
            }
        }
    }
}