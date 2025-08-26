using ConsolePracticeEventDispatcher.Abstracts;
using ConsolePracticeEventDispatcher.Models;

namespace ConsolePracticeEventDispatcher.Services;

public class FakeUserRepository : IUserRepository
{
    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Saving user {user.Id} to fake database.");
        return Task.CompletedTask;
    }
}

