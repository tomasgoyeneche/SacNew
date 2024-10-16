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
            Application.SetHighDpiMode(HighDpiMode.SystemAware); // Mejora la visualización en pantallas de alta resolución.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                using var serviceProvider = Startup.ConfigureServices(); // Asegura la liberación de recursos.

                var loginForm = serviceProvider.GetRequiredService<Login>(); // Lanza excepción si no encuentra el formulario.

                Application.Run(loginForm);
            }
            catch (Exception ex)
            {
                // Manejo global de errores para evitar que la aplicación crashee sin aviso.
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}