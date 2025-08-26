using ConsolePracticeEventDispatcher.Abstracts;
using ConsolePracticeEventDispatcher.Events;

namespace ConsolePracticeEventDispatcher.EventHandlers;

public class UserChangeEmailHandler : IDomainEventHandler<UserChangeEmailEvent>
{
    public async Task HandleAsync(UserChangeEmailEvent @event, CancellationToken cancellationToken)
    {
        await Task.Delay(800, cancellationToken);
        Console.WriteLine($"Change user Email for userId {@event.UserId} , New Email: {@event.Email}");
        await Task.CompletedTask;
    }
}