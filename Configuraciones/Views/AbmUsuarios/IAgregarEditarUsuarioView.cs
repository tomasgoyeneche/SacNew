using Core.Interfaces;
using Shared.Models;

namespace SacNew.Views.Configuraciones.AbmUsuarios
{
    public interface IAgregarEditarUsuarioView : IViewConMensajes
    {
        string Nombre { get; }

        string Usuario { get; }
        string Contrasena { get; }
        string Contrasena2 { get; }

        int Posta { get; }

        void CargarPostas(List<Posta?> postas);

        void MostrarDatosUsuario(Usuario? usuario);
    }
}