using Microsoft.Extensions.DependencyInjection;
using SacNew.Services;

namespace SacNew
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware); // Mejora la visualizaci�n en pantallas de alta resoluci�n.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                using var serviceProvider = Startup.ConfigureServices(); // Asegura la liberaci�n de recursos.

                var loginForm = serviceProvider.GetRequiredService<Login>(); // Lanza excepci�n si no encuentra el formulario.

                Application.Run(loginForm);
            }
            catch (Exception ex)
            {
                // Manejo global de errores para evitar que la aplicaci�n crashee sin aviso.
                MessageBox.Show($"Ocurri� un error inesperado: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}