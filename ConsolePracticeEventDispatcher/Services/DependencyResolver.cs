using System.Reflection;
using ConsolePracticeEventDispatcher.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace ConsolePracticeEventDispatcher.Services;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDomainEventHandlersAuto(this IServiceCollection serviceCollection, Assembly assembly = null)
    {
        // استفاده از اسمبلی فعلی اگر ورودی null باشد
        assembly = assembly ?? Assembly.GetExecutingAssembly();

        // پیدا کردن تمام کلاس‌هایی که IDomainEventHandler<T> را پیاده‌سازی می‌کنند
        var handlerTypes = assembly.GetTypes()
            .Where(type =>
                type.IsClass &&
                !type.IsAbstract &&
                type.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)))
            .ToList();

        Console.WriteLine($"Found {handlerTypes.Count} handler types.");

        foreach (var handlerType in handlerTypes)
        {
            // دریافت اینترفیس‌های ژنریک پیاده‌سازی‌شده
            var interfaces = handlerType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));

            foreach (var interfaceType in interfaces)
            {
                // ثبت سرویس در IServiceCollection
                serviceCollection.AddScoped(interfaceType, handlerType);
                Console.WriteLine($"Registered: {interfaceType.Name} -> {handlerType.Name}");
            }
        }

        return serviceCollection;
    }
}