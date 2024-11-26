using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views;

namespace SacNew.Presenters
{
    public class LoginPresenter : BasePresenter<ILoginView>
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPermisoRepositorio _permisoRepositorio;

        public LoginPresenter(
            IUsuarioRepositorio usuarioRepositorio,
            IPermisoRepositorio permisoRepositorio,
            ISesionService sesionService,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _permisoRepositorio = permisoRepositorio;
        }

        public async Task AutenticarUsuarioAsync()
        {
            string nombreUsuario = _view.NombreUsuario;
            string contrasena = _view.Contrasena;

            await EjecutarConCargaAsync(async () =>
            {
                // Obtener usuario
                Usuario? usuario = await _usuarioRepositorio.ObtenerPorNombreUsuarioAsync(nombreUsuario);

                if (usuario == null || usuario.Contrasena != contrasena || !usuario.Activo)
                {
                    _view.MostrarMensaje("Usuario o contraseña incorrectos, o cuenta inactiva.");
                    return;
                }

                // Obtener permisos
                List<int> permisos = await _permisoRepositorio.ObtenerPermisosPorUsuarioAsync(usuario.IdUsuario);

                // Iniciar sesión
                _sesionService.IniciarSesion(usuario, permisos);

                // Redirigir al menú principal

                await AbrirFormularioAsync<Menu>(async form =>
                {
                    _view.RedirigirAlMenu(form);
                });
            });
        }
    }
}