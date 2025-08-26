using ConsolePracticeEventDispatcher.Abstracts;

namespace ConsolePracticeEventDispatcher.Services;

public class FakeEmailService : IEmailService
{
    public Task SendAsync(string to, string subject, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Fake email sent to {to} with subject: {subject}");
        return Task.CompletedTask;
    }
}