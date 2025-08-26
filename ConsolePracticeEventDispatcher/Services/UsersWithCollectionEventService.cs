using ConsolePracticeEventDispatcher.Abstracts;
using ConsolePracticeEventDispatcher.Events;
using ConsolePracticeEventDispatcher.Models;

namespace ConsolePracticeEventDispatcher.Services;

public class UsersWithCollectionEventService
{
    private readonly IUserRepository _repository;
    private readonly IDomainEventsDispatcher _dispatcher;

    public UsersWithCollectionEventService(IUserRepository repository, IDomainEventsDispatcher dispatcher)
    {
        _repository = repository;
        _dispatcher = dispatcher;
    }

    public async Task RegisterUserAsync(string emailAddress, CancellationToken cancellationToken = default)
    {
        var user = new User()
        {
            Email = emailAddress,
            Id = Guid.NewGuid().ToString(),
        };
        
        await _repository.AddAsync(user, cancellationToken);
        var userRegisterEvent = new UserRegisterEvent(emailAddress, user.Id);
        user.Email = "mojtaba1404.shaghi@gmail.com";
        var userChangeEmailEvent = new UserChangeEmailEvent(user.Id, user.Email);
        // دیسپچ رویدادها بعد از ذخیره
        await _dispatcher.DispatchAsync([userRegisterEvent,userChangeEmailEvent], cancellationToken);

        Console.WriteLine("User registered successfully! And Change Email Address");
    }
}