using Core.Interfaces;
using Shared.Models;

namespace Configuraciones.Views.AbmUsuarios
{
    public interface IPermisosUsuarioView : IViewConMensajes
    {
        void CargarPermisosDisponibles(IEnumerable<Permiso> permisos);

        void CargarPermisosAsignados(IEnumerable<Permiso> permisos);

        Permiso PermisoSeleccionadoDisponible { get; }
        Permiso PermisoSeleccionadoAsignado { get; }
    }
}