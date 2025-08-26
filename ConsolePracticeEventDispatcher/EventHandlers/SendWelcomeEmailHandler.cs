using ConsolePracticeEventDispatcher.Abstracts;
using ConsolePracticeEventDispatcher.Events;

namespace ConsolePracticeEventDispatcher.EventHandlers;

public class SendWelcomeEmailHandler : IDomainEventHandler<UserRegisterEvent>
{
    public async Task HandleAsync(UserRegisterEvent @event, CancellationToken cancellationToken)
    {
        await Task.Delay(2550, cancellationToken);
        Console.WriteLine($"Welcome {@event.Email}");
        await Task.CompletedTask;
    }
}