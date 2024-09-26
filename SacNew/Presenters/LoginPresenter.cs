using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Repositories;
using SacNew.Services;
using SacNew.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Presenters
{
    public class LoginPresenter
    {
        private readonly Login _view;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPermisoRepositorio _permisoRepositorio;
        private readonly ISesionService _sesionService;

        private readonly IServiceProvider _serviceProvider;
        public LoginPresenter(Login view, IUsuarioRepositorio usuarioRepositorio, IPermisoRepositorio permisoRepositorio, ISesionService sesionService, IServiceProvider serviceProvider)
        {
            _view = view;
            _usuarioRepositorio = usuarioRepositorio;
            _permisoRepositorio = permisoRepositorio;
            _sesionService = sesionService;
            _serviceProvider = serviceProvider;
        }

        public void AutenticarUsuario()
        {
            string nombreUsuario = _view.NombreUsuario;
            string contrasena = _view.Contrasena;

            Usuario usuario = _usuarioRepositorio.ObtenerPorNombreUsuario(nombreUsuario);

            if (usuario == null || usuario.Contrasena != contrasena || !usuario.Activo)
            {
                _view.MostrarMensaje("Usuario o contraseña incorrectos, o cuenta inactiva.");
            }
            else
            {
                // Aquí puedes manejar los permisos o redirigir al menú
                List<int> permisos = _permisoRepositorio.ObtenerPermisosPorUsuario(usuario.IdUsuario);

                _sesionService.IniciarSesion(usuario, permisos);

                var menuForm = _serviceProvider.GetService<Menu>();
                _view.RedirigirAlMenu(menuForm);
            }
        }


       
    }
}
