namespace ConsolePracticeEventDispatcher.Abstracts;

public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OccuredOn { get; }
}