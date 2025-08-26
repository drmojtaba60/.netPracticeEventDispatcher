namespace ConsolePracticeEventDispatcher.Abstracts;

public interface IDomainEventDispatcher
{
    Task DispatchAsync<TEvent>(TEvent domainEvent,CancellationToken cancellationToken) where TEvent : IDomainEvent;
}

public interface IDomainEventsDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}