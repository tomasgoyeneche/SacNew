using Microsoft.Extensions.DependencyInjection;
using SacNew.Presenters;
using SacNew.Repositories;
using SacNew.Views;
using SacNew.Views.GestionFlota.Postas;
using SacNew.Views.GestionFlota.Postas.ABMPostas;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;

namespace SacNew.Services
{
    public class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ISesionService, SesionService>();

            // Registrar Repositorios
            serviceCollection.AddSingleton<IUsuarioRepositorio, UsuarioRepositorio>();
            serviceCollection.AddSingleton<IPermisoRepositorio, PermisoRepositorio>();
            serviceCollection.AddSingleton<IPostaRepositorio, PostaRepositorio>();
            serviceCollection.AddSingleton<IProvinciaRepositorio, ProvinciaRepositorio>();  // Registrar el repositorio de provincias
            serviceCollection.AddSingleton<IProvinciaRepositorio, ProvinciaRepositorio>();  // Registrar el repositorio de provincias
            serviceCollection.AddSingleton<IConceptoRepositorio, ConceptoRepositorio>();
            serviceCollection.AddSingleton<IConceptoTipoRepositorio, ConceptoTipoRepositorio>();

            // Registrar Formularios
            serviceCollection.AddTransient<Login>();
            serviceCollection.AddTransient<Menu>();
            serviceCollection.AddTransient<MenuPostas>();
            serviceCollection.AddTransient<MenuAbmPostas>();
            serviceCollection.AddTransient<AgregarEditarPosta>();  // Registrar el Formulario de Agregar Posta
            serviceCollection.AddTransient<MenuConceptos>();
            serviceCollection.AddTransient<MenuConceptos>();
            serviceCollection.AddTransient<AgregarEditarConcepto>();
            //Presentadores
            serviceCollection.AddTransient<MenuAbmPostasPresenter>();  // Presenter de Postas
            serviceCollection.AddTransient<AgregarEditarPostaPresenter>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}