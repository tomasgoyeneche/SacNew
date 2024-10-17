using SacNew.Views;

namespace SacNew.Interfaces
{
    public interface ILoginView : IViewConMensajes
    {
        string NombreUsuario { get; }
        string Contrasena { get; }

        void RedirigirAlMenu(Menu menuform);
    }
}