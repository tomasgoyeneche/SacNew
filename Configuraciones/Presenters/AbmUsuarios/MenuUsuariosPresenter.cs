using Core.Base;
using Core.Repositories;
using Core.Services;
using SacNew.Views.Configuraciones.AbmUsuarios;

namespace SacNew.Presenters.AbmUsuarios
{
    public class MenuUsuariosPresenter : BasePresenter<IMenuUsuariosView>
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public MenuUsuariosPresenter(
            IUsuarioRepositorio usuarioRepositorio,
            ISesionService sesionService,
           INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task InicializarAsync()
        {
            await CargarUsuariosAsync();
        }

        public async Task CargarUsuariosAsync()
        {
            await EjecutarConCargaAsync(
                () => _usuarioRepositorio.ObtenerTodosAsync(),
                _view.CargarUsuarios
            );
        }

        public async Task EliminarUsuarioAsync(int idUsuario)
        {
            var confirmacion = _view.ConfirmarEliminacion("¿Está seguro que desea eliminar este usuario?");
            if (confirmacion == DialogResult.Yes)
            {
                await EjecutarConCargaAsync(async () =>
                {
                    await _usuarioRepositorio.EliminarAsync(idUsuario);
                    _view.MostrarMensaje("Usuario eliminado correctamente.");
                }, CargarUsuariosAsync);
            }
        }

        public async Task BuscarUsuariosAsync(string criterio)
        {
            await EjecutarConCargaAsync(
                () => _usuarioRepositorio.BuscarPorCriterioAsync(criterio),
                _view.CargarUsuarios
            );
        }

        public async Task AgregarUsuario()
        {
            await AbrirFormularioAsync<AgregarEditarUsuarioForm>(async form =>
            {
                await form._presenter.InicializarAsync(null);
            });
            await CargarUsuariosAsync(); // Refrescar al cerrar el formulario
        }

        public async Task EditarUsuario(int idUsuario)
        {
            await AbrirFormularioAsync<AgregarEditarUsuarioForm>(async form =>
            {
                await form._presenter.InicializarAsync(idUsuario);
            });
            await CargarUsuariosAsync(); // Refrescar al cerrar el formulario
        }
    }
}