using ConsolePracticeEventDispatcher.Models;

namespace ConsolePracticeEventDispatcher.Abstracts;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);
}