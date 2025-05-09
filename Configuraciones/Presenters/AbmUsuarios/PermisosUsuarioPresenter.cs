using Configuraciones.Views.AbmUsuarios;
using Core.Base;
using Core.Repositories;
using Core.Services;
using SacNew.Views.Configuraciones.AbmLocaciones;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuraciones.Presenters
{
    public class PermisosUsuarioPresenter : BasePresenter<IPermisosUsuarioView>
    {
        private readonly IPermisoRepositorio _permisoRepositorio;
        private int _idUsuario;

        public PermisosUsuarioPresenter(
            IPermisoRepositorio permisoRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _permisoRepositorio = permisoRepositorio;
        }

        public async Task InicializarAsync(int idUsuario)
        {
            _idUsuario = idUsuario;

            await EjecutarConCargaAsync(async () =>
            {
                List<Permiso> todosLosPermisos = await _permisoRepositorio.ObtenerTodoslosPermisos();
                List<Permiso> permisosAsignados = await _permisoRepositorio.ObtenerPermisosPorUsuarioAsync(idUsuario);

                var idsAsignadas = permisosAsignados.Select(l => l.IdPermiso).ToHashSet();
                var permisosDisponibles = todosLosPermisos.Where(l => !idsAsignadas.Contains(l.IdPermiso));

                _view.CargarPermisosDisponibles(permisosDisponibles);
                _view.CargarPermisosAsignados(permisosAsignados);
            });
        }

        public async Task AgregarPermisoAsync()
        {
            Permiso permisoSeleccionado = _view.PermisoSeleccionadoDisponible;
            if (permisoSeleccionado == null)
            {
                _view.MostrarMensaje("Debe seleccionar un permiso disponible.");
                return;
            }

            UsuarioPermiso Permiso = new UsuarioPermiso
            {
                IdUsuario = _idUsuario,
                IdPermiso = permisoSeleccionado.IdPermiso,
            };

            await EjecutarConCargaAsync(
                () => _permisoRepositorio.AgregarPermisoAsync(Permiso),
                async () => await InicializarAsync(_idUsuario)
            );
        }

        public async Task EliminarPermisoAsync()
        {
            Permiso permisoSeleccionado = _view.PermisoSeleccionadoAsignado;
            if (permisoSeleccionado == null)
            {
                _view.MostrarMensaje("Debe seleccionar una permiso asignado.");
                return;
            }

            UsuarioPermiso permisoUsuario = await _permisoRepositorio.ObtenerRelacionAsync(_idUsuario, permisoSeleccionado.IdPermiso);
            if (permisoUsuario == null)
            {
                _view.MostrarMensaje("No se encontró la relación para eliminar.");
                return;
            }

            await EjecutarConCargaAsync(
                () => _permisoRepositorio.EliminarPermisoAsync(permisoUsuario),
                async () => await InicializarAsync(_idUsuario)
            );
        }
    }
}
