using App.Views;
using Core.Base;
using Core.Repositories;
using Core.Services;
using SacNew.Views;
using Shared.Models;
using System.Diagnostics;

namespace App.Presenters
{
    public class LoginPresenter : BasePresenter<ILoginView>
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IPermisoRepositorio _permisoRepositorio;
        private readonly IVersionRepositorio _versionRepositorio;

        private const string VERSION_ACTUAL = "0.1";
        private string NOMBRE_CARPETA_VERSION;
        private string RUTA_REMOTA;
        private const string RUTA_LOCAL = @"C:\Compartida\SACNew";
        public LoginPresenter(
            IUsuarioRepositorio usuarioRepositorio,
            IPermisoRepositorio permisoRepositorio,
            ISesionService sesionService,
            IVersionRepositorio versionRepositorio,
            INavigationService navigationService)
            : base(sesionService, navigationService)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _permisoRepositorio = permisoRepositorio;
            _versionRepositorio = versionRepositorio;
        }

        public async Task AutenticarUsuarioAsync()
        {

            Versiones versionBD = await _versionRepositorio.ObtenerVersionActivaAsync();
            NOMBRE_CARPETA_VERSION = "SACNew" + versionBD.NumeroVersion;    
            RUTA_REMOTA = @"S:\" + NOMBRE_CARPETA_VERSION; 

            if (versionBD == null || versionBD.NumeroVersion != VERSION_ACTUAL)
            {
                var mensaje = versionBD == null
                    ? "No se pudo obtener la versión activa del sistema."
                    : $"Hay una nueva versión disponible ({versionBD.NumeroVersion}). ¿Desea actualizar ahora?";

                var resultado = MessageBox.Show(mensaje, "Actualización disponible",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                {
                    Application.Exit();
                    return;
                }

                // Validar que exista la carpeta de versión nueva en Q:
                if (!Directory.Exists(RUTA_REMOTA))
                {
                    MessageBox.Show("La nueva versión no está disponible. Avisar al sector de sistemas.",
                        "Error de actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }

                try
                {
                    // Cerrar procesos abiertos (opcional)
                    string exeActual = Application.ExecutablePath;
                    string nombreExe = Path.GetFileName(exeActual);

                    // Eliminar proceso actual
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/C timeout 2 & taskkill /F /IM \"{nombreExe}\" & xcopy \"{RUTA_REMOTA}\" \"{RUTA_LOCAL}\" /E /Y /I & start \"\" \"{Path.Combine(RUTA_LOCAL, nombreExe)}\"",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true
                    });

                    Application.Exit();
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error durante la actualización: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
            }






















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
                List<String> permisos = await _permisoRepositorio.ObtenerPermisosPorUsuarioAsync(usuario.IdUsuario);

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