using Core.Interfaces;
using SacNew.Views;

namespace App.Views
{
    public interface ILoginView : IViewConMensajes
    {
        string NombreUsuario { get; }
        string Contrasena { get; }

        void RedirigirAlMenu(Menu menuform);
    }
}