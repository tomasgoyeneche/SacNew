using SacNew.Interfaces;
using SacNew.Models;

namespace SacNew.Views.Configuraciones.AbmUsuarios
{
    public interface IMenuUsuariosView : IViewConMensajes
    {
        string CriterioBusqueda { get; }

        void CargarUsuarios(List<Usuario> usuarios);  // Trabajamos directamente con la entidad

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}