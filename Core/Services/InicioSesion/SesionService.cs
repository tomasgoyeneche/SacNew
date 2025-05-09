using Shared.Models;

namespace Core.Services
{
    public class SesionService : ISesionService
    {
        public int IdUsuario { get; private set; }
        public string? NombreCompleto { get; private set; }
        public List<String> Permisos { get; private set; }

        public int IdPosta { get; private set; }

        public void IniciarSesion(Usuario usuario, List<String> permisos)
        {
            IdUsuario = usuario.IdUsuario;
            NombreCompleto = usuario.NombreCompleto;
            Permisos = permisos;
            IdPosta = usuario.idPosta;
        }

        public void CerrarSesion()
        {
            IdUsuario = 0;
            NombreCompleto = string.Empty;
            Permisos = new List<String>();
        }
    }
}