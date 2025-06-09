using Shared.Models;

namespace Core.Services
{
    public interface ISesionService
    {
        int IdUsuario { get; }
        string? NombreCompleto { get; }
        string? NombreUsuario { get; }

        List<string>? Permisos { get; }

        int IdPosta { get; }

        void IniciarSesion(Usuario usuario, List<String> permisos);

        void CerrarSesion();
    }
}