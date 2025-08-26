using ConsolePracticeEventDispatcher.Abstracts;

namespace ConsolePracticeEventDispatcher.Events;

public record UserChangeEmailEvent(string Email, string UserId) : IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccuredOn => DateTime.Now;
}