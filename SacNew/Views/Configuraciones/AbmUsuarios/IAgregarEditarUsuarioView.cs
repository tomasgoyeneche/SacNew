using SacNew.Interfaces;
using SacNew.Models;

namespace SacNew.Views.Configuraciones.AbmUsuarios
{
    public interface IAgregarEditarUsuarioView : IViewConMensajes
    {
        string Nombre { get; }

        string Usuario { get; }
        string Contrasena { get; }
        int Posta { get; }

        void CargarPostas(List<Posta> postas);

        void MostrarDatosUsuario(Usuario usuario);
    }
}