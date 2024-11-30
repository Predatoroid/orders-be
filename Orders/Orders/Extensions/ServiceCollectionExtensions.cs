using System.Reflection;
using Orders.Abstractions;

namespace Orders.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesByReflection(this IServiceCollection services, Assembly assembly)
    {
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
}