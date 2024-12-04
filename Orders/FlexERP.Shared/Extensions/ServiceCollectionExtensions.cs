using System.Reflection;
using FlexERP.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FlexERP.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesByReflection(this IServiceCollection services, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        
        // Get all types in the specified assembly
        var types = assembly.GetTypes();

        // Filter types that implement IService (excluding interfaces and abstract classes)
        var serviceTypes = types.Where(t => typeof(IService).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

        foreach (var type in serviceTypes)
        {
            // Register each type as itself and its interfaces
            var interfaces = type.GetInterfaces().Where(i => i != typeof(IService));
            foreach (var iface in interfaces)
            {
                services.AddScoped(iface, type); // Register with DI
            }
            
            // Optionally, register the type itself (without an interface)
            services.AddScoped(type);
        }

        return services;
    }
    
    public static IServiceCollection AddRepositoriesByReflection(this IServiceCollection services, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var types = assembly.GetTypes();
        var repositoryTypes = types.Where(t => typeof(IRepository).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

        foreach (var type in repositoryTypes)
        {
            var interfaces = type.GetInterfaces().Where(i => i != typeof(IRepository));
            foreach (var iface in interfaces)
            {
                services.AddSingleton(iface, type); // Register with DI as Singleton
            }

            // Optionally, register the type itself (without an interface)
            services.AddSingleton(type);
        }

        return services;
    }
}