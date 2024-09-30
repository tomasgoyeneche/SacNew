using SacNew.Models;

namespace SacNew.Services
{
    public interface ISesionService
    {
        int IdUsuario { get; }
        string NombreCompleto { get; }
        List<int> Permisos { get; }

        void IniciarSesion(Usuario usuario, List<int> permisos);

        void CerrarSesion();
    }
}