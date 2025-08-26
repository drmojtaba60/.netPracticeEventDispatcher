using Microsoft.Extensions.DependencyInjection;
using ConsolePracticeEventDispatcher.Abstracts;

namespace ConsolePracticeEventDispatcher.Dispatcher;

public class EventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{

    public async Task DispatchAsync<TEvent>(TEvent domainEvent,CancellationToken cancellationToken) where TEvent : IDomainEvent
    {
       var handlers=  serviceProvider.GetServices<IDomainEventHandler<TEvent>>();
       foreach (var handler in handlers)
       {
          await handler.HandleAsync(domainEvent,cancellationToken);
       }
    }

}