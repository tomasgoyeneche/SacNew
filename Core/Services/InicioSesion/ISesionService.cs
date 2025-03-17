using Shared.Models;

namespace Core.Services
{
    public interface ISesionService
    {
        int IdUsuario { get; }
        string NombreCompleto { get; }
        List<int> Permisos { get; }

        int IdPosta { get; }

        void IniciarSesion(Usuario usuario, List<int> permisos);

        void CerrarSesion();
    }
}