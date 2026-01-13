namespace Shared.Models.RequerimientoCompra
{
    public class UsuarioRcc
    {
        public int IdUsuario { get; set; }
        public string UsuarioLogin { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string NombreApellido { get; set; } = string.Empty;
        public string Funcion { get; set; } = string.Empty;
        public bool Administrador { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}