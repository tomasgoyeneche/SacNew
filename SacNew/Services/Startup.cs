using Microsoft.Extensions.DependencyInjection;
using SacNew.Interfaces;
using SacNew.Presenters;
using SacNew.Repositories;
using SacNew.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Services
{
    public class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // Registrar Repositorios
            serviceCollection.AddSingleton<IUsuarioRepositorio, UsuarioRepositorio>();
            serviceCollection.AddSingleton<IPermisoRepositorio, PermisoRepositorio>();


            serviceCollection.AddSingleton<ISesionService, SesionService>();


            // Registrar MenuForm
            serviceCollection.AddTransient<Login>();
            serviceCollection.AddTransient<Menu>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
