using ConsolePracticeEventDispatcher.Abstracts;

namespace ConsolePracticeEventDispatcher.Events;

public record UserRegisterEvent(string Email,string UserId):IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccuredOn => DateTime.Now;
}