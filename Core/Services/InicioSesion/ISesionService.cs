using Shared.Models;

namespace Core.Services
{
    public interface ISesionService
    {
        int IdUsuario { get; }
        string NombreCompleto { get; }
        List<String> Permisos { get; }

        int IdPosta { get; }

        void IniciarSesion(Usuario usuario, List<String> permisos);

        void CerrarSesion();
    }
}