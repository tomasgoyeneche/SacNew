using Configuraciones;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using Microsoft.Extensions.DependencyInjection;
using SacNew;
using System.Globalization;

namespace App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ConfigurarCultura();
            ConfigurarManejoGlobalExcepciones();

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("The Bezier");  // Cambiá por el que quieras //  WXI // The Bezier bastante bien

            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                //ReportSchemaGenerator.GenerarEsquemaReportes();
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var serviceProvider = Startup.ConfigureServices();
                var loginForm = serviceProvider.GetRequiredService<Login>();
                Application.Run(loginForm);
            }
            catch (Exception ex)
            {
                MostrarErrorCritico(ex);
            }
        }

        private static void ConfigurarCultura()
        {
            var cultura = new System.Globalization.CultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentCulture = cultura;
            CultureInfo.DefaultThreadCurrentUICulture = cultura;
        }

        private static void ConfigurarManejoGlobalExcepciones()
        {
            // Manejo de excepciones en hilos de la interfaz gráfica
            Application.ThreadException += (sender, e) =>
                MostrarMensajeDeError("Error no controlado", e.Exception);

            // Manejo de excepciones críticas del dominio de la aplicación
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    MostrarMensajeDeError("Error fatal", ex);
                }
            };

            // Manejo de excepciones no observadas en tareas asíncronas
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                MostrarMensajeDeError("Excepción no observada en tarea", e.Exception);
                e.SetObserved();  // Marcar como observada para evitar cierre inesperado
            };
        }

        private static void MostrarMensajeDeError(string titulo, Exception ex)
        {
            MessageBox.Show(
                $"{titulo}: {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        private static void MostrarErrorCritico(Exception ex)
        {
            // Mostrar el mensaje de error crítico
            MessageBox.Show(
                $"Error crítico: {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            // Establecer código de salida y cerrar la aplicación
            Environment.ExitCode = 1;
            Environment.Exit(1);
        }
    }
}