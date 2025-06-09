using Shared.Models;

namespace Core.Services
{
    public class SesionService : ISesionService
    {
        public int IdUsuario { get; private set; }
        public string? NombreCompleto { get; private set; } // Changed from string? to string
        public string? NombreUsuario{ get; private set; } // Changed from string? to string

        public List<string>? Permisos { get; private set; }

        public int IdPosta { get; private set; }

        public void IniciarSesion(Usuario usuario, List<string> permisos)
        {
            IdUsuario = usuario.IdUsuario;
            NombreCompleto = usuario.NombreCompleto ?? string.Empty; // Ensure non-null value
            NombreUsuario = usuario.NombreUsuario ?? string.Empty; // Ensure non-null value
            Permisos = permisos;
            IdPosta = usuario.idPosta;
        }

        public void CerrarSesion()
        {
            IdUsuario = 0;
            NombreCompleto = string.Empty;
            Permisos = new List<string>();
        }
    }
}