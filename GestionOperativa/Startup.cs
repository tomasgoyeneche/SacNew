using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models;
using System.IO;
using System.Reflection;

namespace Configuraciones
{
    public class Startup
    {
        public static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // Configuración general: registrar el proveedor de cadenas de conexión
            var connectionStringProvider = new ConnectionStringProvider();
            var connectionStrings = connectionStringProvider.GetConnectionStrings();
            serviceCollection.AddSingleton(connectionStrings);

            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                            .Select(Assembly.LoadFrom)
                            .ToArray();

            // Registro automático de dependencias
            serviceCollection.Scan(scan => scan                        /*                                                 */
                .FromAssemblies(assemblies)

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


                .AddClasses(classes =>
                classes.Where(type =>
                    type.IsSubclassOf(typeof(Form)) &&
                    !type.IsGenericTypeDefinition &&
                    !type.Assembly.GetName().Name.StartsWith("DevExpress")
                ))
                .AsSelf()
                .AsImplementedInterfaces()
                .WithTransientLifetime()



                //.AddClasses(classes => classes.Where(type => type.IsSubclassOf(typeof(Form))))
                //    .AsSelf()
                //    .AsImplementedInterfaces()
                //    .WithTransientLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
            );

            return serviceCollection.BuildServiceProvider();
        }
    }
}