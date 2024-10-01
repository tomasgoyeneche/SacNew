using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Presenters;
using SacNew.Repositories;
using SacNew.Services;

namespace SacNew
{
    internal static class Program
    {


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var serviceProvider = Startup.ConfigureServices();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Resolver LoginForm directamente desde el contenedor
            var loginForm = serviceProvider.GetService<Login>();

            //var loginPresenter = new LoginPresenter(
            //  loginForm,
            //  serviceProvider.GetService<IUsuarioRepositorio>(),
            //  serviceProvider.GetService<IPermisoRepositorio>(),
            //  serviceProvider.GetService<ISesionService>(),
            //  serviceProvider);

            //// Inyectar manualmente el presentador en el LoginForm
            //loginForm.SetPresenter(loginPresenter);
            Application.Run(loginForm);

        }


    }
}