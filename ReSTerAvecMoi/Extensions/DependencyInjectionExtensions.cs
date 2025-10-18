using ReSTerAvecMoi.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ReSTerAvecMoi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAttributedServices(this IServiceCollection services, Assembly assembly)
    {
        var typesWithAttribute = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<InjectForAttribute>() != null);

        foreach (var type in typesWithAttribute)
        {
            var attr = type.GetCustomAttribute<InjectForAttribute>();
            if (attr?.InterfaceType != null) services.AddTransient(attr.InterfaceType, type);
            else throw new Exception($"No attribute found for {type.Name}");
        }
    }
}