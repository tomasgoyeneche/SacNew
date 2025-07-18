using Core.Base;
using Core.Repositories;
using Core.Services;
using SacNew.Views.Configuraciones.AbmUsuarios;
using Shared.Models;

namespace SacNew.Presenters.AbmUsuarios
{
    public class AgregarEditarUsuarioPresenter : BasePresenter<IAgregarEditarUsuarioView>
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPostaRepositorio _postaRepositorio;

        private Usuario? _usuarioActual;

        public AgregarEditarUsuarioPresenter(
            IUsuarioRepositorio usuarioRepositorio,
            IPostaRepositorio postaRepositorio,
            ISesionService sesionService,
            INavigationService navigationService
        ) : base(sesionService, navigationService)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _postaRepositorio = postaRepositorio;
        }

        public async Task InicializarAsync(int? idUsuario)
        {
            await EjecutarConCargaAsync(async () =>
            {
                await CargarPostasAsync();

                if (idUsuario.HasValue)
                {
                    _usuarioActual = await _usuarioRepositorio.ObtenerPorIdAsync(idUsuario.Value);

                    _view.MostrarDatosUsuario(_usuarioActual);
                }
                else
                {
                    _usuarioActual = new Usuario();
                }
            });
        }

        public async Task CargarPostasAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                var postas = await _postaRepositorio.ObtenerTodasLasPostasAsync();
                _view.CargarPostas(postas);
            });
        }

        public async Task GuardarUsuarioAsync()
        {
            await EjecutarConCargaAsync(async () =>
            {
                _usuarioActual.NombreUsuario = _view.Usuario;
                _usuarioActual.NombreCompleto = _view.Nombre;
                _usuarioActual.idPosta = _view.Posta;
                _usuarioActual.Activo = true;

                if (!string.IsNullOrWhiteSpace(_view.Contrasena))
                {
                    if (_view.Contrasena != _view.Contrasena2)
                    {
                        _view.MostrarMensaje("Las contraseñas no coinciden.");
                        return;
                    }

                    _usuarioActual.Contrasena = _view.Contrasena;
                }

                if (_usuarioActual.IdUsuario == 0)
                {
                    await _usuarioRepositorio.AgregarAsync(_usuarioActual);
                    _view.MostrarMensaje("Usuario agregado correctamente.");
                }
                else
                {
                    await _usuarioRepositorio.ActualizarAsync(_usuarioActual);
                    _view.MostrarMensaje("Usuario actualizado correctamente.");
                }
            });
        }
    }
}