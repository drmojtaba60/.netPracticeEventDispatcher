using System.Reflection;
using ConsolePracticeEventDispatcher.Abstracts;
using ConsolePracticeEventDispatcher.Dispatcher;
using ConsolePracticeEventDispatcher.EventHandlers;
using ConsolePracticeEventDispatcher.Events;
using ConsolePracticeEventDispatcher.Services;
using Microsoft.Extensions.DependencyInjection;

// اکستنشن متد برای ثبت خودکار هندلرها
Console.WriteLine("Hello World!Dispatching Events User Register");

var services = new ServiceCollection();
services=RegisterServices();
var serviceProvider = services.BuildServiceProvider();
var random = new Random().Next(1,100);
if (random % 2 == 0)
    await GetUserService(serviceProvider);
else await GetUserCollectionEventService(serviceProvider);

Console.WriteLine("press any key to exit");
Console.ReadKey();
return;

async Task GetUserService(ServiceProvider serviceProvider)
{
    var userService = serviceProvider.GetRequiredService<UserService>();
    await userService?.UserServiceAsync("mojtaba.shaghi@gmail.com",Guid.NewGuid().ToString())!;
}

async Task GetUserCollectionEventService(ServiceProvider serviceProvider)
{
    var userService = serviceProvider.GetRequiredService<UsersWithCollectionEventService>();
    await userService?.RegisterUserAsync("mojtaba.shaghi@gmail.com")!;
}
ServiceCollection RegisterServices()
{
    var serviceCollection =new ServiceCollection();
    serviceCollection.AddScoped<IDomainEventDispatcher,EventDispatcher>();
    serviceCollection.AddScoped<IDomainEventsDispatcher,DomainEventsDispatcher>();
    serviceCollection.RegisterDomainEventHandlersAuto();
    //RegisterDomainEventHandlersAuto(serviceCollection);
    //RegisterDomainEventHandlersManual(serviceCollection);
    
    serviceCollection.AddScoped<IUserRepository,FakeUserRepository>();
    serviceCollection.AddScoped<IEmailService, FakeEmailService>();
    
    serviceCollection.AddScoped<UserService>();
    serviceCollection.AddScoped<UsersWithCollectionEventService>();
    
    return serviceCollection;
}

void RegisterDomainEventHandlersManual(ServiceCollection serviceCollection)
{
    serviceCollection.AddScoped<IDomainEventHandler<UserRegisterEvent>,SendWelcomeEmailHandler>();
    serviceCollection.AddScoped<IDomainEventHandler<UserRegisterEvent>,TrackUserRegistrationHandler>();
    serviceCollection.AddScoped<IDomainEventHandler<UserChangeEmailEvent>,UserChangeEmailHandler>();

}



void RegisterDomainEventHandlersAuto(ServiceCollection serviceCollection)
{
    // دریافت اسمبلی فعلی
    Assembly assembly = Assembly.GetExecutingAssembly();
    // پیدا کردن تمام کلاس‌هایی که IDomainEventHandler<T> را پیاده‌سازی می‌کنند
    var handlerTypes = assembly.GetTypes()
        .Where(type =>
            type.IsClass &&
            !type.IsAbstract &&
            type.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)))
        .ToList();

    foreach (var handlerType in handlerTypes)
    {
        // دریافت اینترفیس‌های ژنریک پیاده‌سازی‌شده
        var interfaces = handlerType.GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>));

        foreach (var interfaceType in interfaces)
        {
            // ثبت سرویس در IServiceCollection
            serviceCollection.AddScoped(interfaceType, handlerType);
        }
    }
}







