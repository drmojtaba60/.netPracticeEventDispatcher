using ConsolePracticeEventDispatcher.Abstracts;
using ConsolePracticeEventDispatcher.Events;

namespace ConsolePracticeEventDispatcher.EventHandlers;

public class TrackUserRegistrationHandler : IDomainEventHandler<UserRegisterEvent>
{
    public async Task HandleAsync(UserRegisterEvent @event, CancellationToken cancellationToken)
    {
        await Task.Delay(2500, cancellationToken);
        Console.WriteLine($"Tracking user registration for userId {@event.UserId}");
        await Task.CompletedTask;
    }
}