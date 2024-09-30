using SacNew.Views;

namespace SacNew.Interfaces
{
    public interface ILoginView
    {
        string NombreUsuario { get; }
        string Contrasena { get; }

        void MostrarMensaje(string mensaje);

        void RedirigirAlMenu(Menu menuform);
    }
}