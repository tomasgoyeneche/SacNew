using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace SacNew.Services
{
    public class Startup
    {
        public static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // Configuración general
            string connectionString = ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString;
            serviceCollection.AddSingleton(connectionString);

            // Registro automático de dependencias
            serviceCollection.Scan(scan => scan
                .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())

                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repositorio")))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()

                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()

                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Processor")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()

                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Presenter")))
                    .AsSelf()
                    .WithTransientLifetime()

                .AddClasses(classes => classes.Where(type => type.IsSubclassOf(typeof(Form))))
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
            );

            return serviceCollection.BuildServiceProvider();
        }
    }
}