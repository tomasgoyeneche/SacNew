using Microsoft.Extensions.DependencyInjection;
using SacNew.Presenters;
using SacNew.Repositories;
using SacNew.Views;
using SacNew.Views.Configuraciones.AbmLocaciones;
using SacNew.Views.GestionFlota.Postas;
using SacNew.Views.GestionFlota.Postas.ABMPostas;
using SacNew.Views.GestionFlota.Postas.ConceptoConsumos;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos;
using SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc;
using System.Configuration;

namespace SacNew.Services
{
    public class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ISesionService, SesionService>();

            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
            // Reemplaza con tu cadena de conexión real
            serviceCollection.AddSingleton(connectionString);

            // Registrar Repositorios
            serviceCollection.AddSingleton<IUsuarioRepositorio, UsuarioRepositorio>();
            serviceCollection.AddSingleton<IPermisoRepositorio, PermisoRepositorio>();
            serviceCollection.AddSingleton<IPostaRepositorio, PostaRepositorio>();
            serviceCollection.AddSingleton<IProvinciaRepositorio, ProvinciaRepositorio>();  // Registrar el repositorio de provincias
            serviceCollection.AddSingleton<IProvinciaRepositorio, ProvinciaRepositorio>();  // Registrar el repositorio de provincias
            serviceCollection.AddSingleton<IConceptoRepositorio, ConceptoRepositorio>();
            serviceCollection.AddSingleton<IConceptoTipoRepositorio, ConceptoTipoRepositorio>();
            serviceCollection.AddSingleton<IConceptoProveedorRepositorio, ConceptoProveedorRepositorio>();
            serviceCollection.AddSingleton<IConceptoPostaProveedorRepositorio, ConceptoPostaProveedorRepositorio>();
            serviceCollection.AddSingleton<IRepositorioPOC, RepositorioPOC>();
            serviceCollection.AddSingleton<INominaRepositorio, NominaRepositorio>();
            serviceCollection.AddSingleton<ILocacionRepositorio, LocacionRepositorio>();
            serviceCollection.AddSingleton<ILocacionKilometrosEntreRepositorio, LocacionKilometrosEntreRepositorio>();
            serviceCollection.AddSingleton<ILocacionProductoRepositorio, LocacionProductoRepositorio>();
            serviceCollection.AddSingleton<IProductoRepositorio, ProductoRepositorio>();

            serviceCollection.AddSingleton<IAuditoriaRepositorio, AuditoriaRepositorio>();
            serviceCollection.AddSingleton<IAuditoriaService, AuditoriaService>();

            // Registrar Formularios
            serviceCollection.AddTransient<Login>();
            serviceCollection.AddTransient<Menu>();
            serviceCollection.AddTransient<MenuPostas>();
            serviceCollection.AddTransient<MenuAbmPostas>();
            serviceCollection.AddTransient<AgregarEditarPosta>();
            serviceCollection.AddTransient<MenuConceptos>();
            serviceCollection.AddTransient<AgregarEditarConcepto>();
            serviceCollection.AddTransient<MenuIngresaConsumos>();
            serviceCollection.AddTransient<AgregarEditarPoc>();
            serviceCollection.AddTransient<MenuLocaciones>();
            serviceCollection.AddTransient<AgregarEditarLocacion>();
            serviceCollection.AddTransient<AgregarProductoForm>();
            //Presentadores
            serviceCollection.AddTransient<MenuAbmPostasPresenter>();  // Presenter de Postas
            serviceCollection.AddTransient<AgregarEditarPostaPresenter>();
            serviceCollection.AddTransient<AgregarEditarConceptoPresenter>();
            serviceCollection.AddTransient<MenuIngresaConsumosPresenter>();
            serviceCollection.AddTransient<AgregarEditarPOCPresenter>();
            serviceCollection.AddTransient<MenuLocacionesPresenter>();
            serviceCollection.AddTransient<AgregarEditarLocacionPresenter>();
            serviceCollection.AddTransient<AgregarProductoPresenter>();
            return serviceCollection.BuildServiceProvider();
        }
    }
}