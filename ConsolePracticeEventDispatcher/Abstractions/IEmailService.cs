namespace ConsolePracticeEventDispatcher.Abstracts;

public interface IEmailService
{
    Task SendAsync(string to, string subject, CancellationToken cancellationToken = default);
}