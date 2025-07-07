using Core.Interfaces;
using Shared.Models;

namespace SacNew.Views.Configuraciones.AbmUsuarios
{
    public interface IMenuUsuariosView : IViewConMensajes
    {
        void CargarUsuarios(List<Usuario> usuarios);  // Trabajamos directamente con la entidad

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}