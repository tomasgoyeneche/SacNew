using SacNew.Interfaces;

namespace SacNew.Views
{
    public interface ILoginView : IViewConMensajes
    {
        string NombreUsuario { get; }
        string Contrasena { get; }
        void RedirigirAlMenu(Menu menuform);
    }
}