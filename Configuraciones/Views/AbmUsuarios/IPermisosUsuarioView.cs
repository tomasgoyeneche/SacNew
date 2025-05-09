using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
