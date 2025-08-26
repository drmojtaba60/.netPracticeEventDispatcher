using ConsolePracticeEventDispatcher.Abstracts;
using ConsolePracticeEventDispatcher.EventHandlers;
using ConsolePracticeEventDispatcher.Events;
using ConsolePracticeEventDispatcher.Models;

namespace ConsolePracticeEventDispatcher.Services;

public class UserService(IDomainEventDispatcher dispatcher,IUserRepository  userRepository,IEmailService emailService) : IUserService
{
    public async Task UserServiceAsync(string email, string userId)
    {
        await userRepository.AddAsync(new User() { Id = userId, Email = email });
        await emailService.SendAsync("google.gmail.com", email);
        await dispatcher.DispatchAsync(new UserRegisterEvent(email, userId), CancellationToken.None);
        Console.WriteLine("User registered successfully!");
    }
}

public interface IUserService
{
    Task UserServiceAsync(string email, string userId);
}