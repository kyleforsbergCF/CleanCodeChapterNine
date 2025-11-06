using ChapterNine.Application.Exceptions;
using ChapterNine.Application.Interfaces;
using ChapterNine.Application.Models;

namespace ChapterNine.Application.Services;

public class UserService(IUserRepository userRepository, ICacheService cacheService, IUserValidator userValidator) : IUserService
{
    public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var users = await cacheService.GetValueAsync<IEnumerable<User>>("users", cancellationToken);
        if (users == null || !users.Any())
        {
            users = await userRepository.GetAllAsync(cancellationToken);
            await cacheService.SetValueAsync("users", users, cancellationToken);
        }
        return users ?? [];
    }

    public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        if (!userValidator.IsValid(user))
        {
            throw new InvalidUserException();
        }
        return await userRepository.CreateUserAsync(user, cancellationToken);
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        if (!userValidator.IsValid(user))
        {
            throw new InvalidUserException();
        }
        return await userRepository.UpdateUserAsync(user, cancellationToken);
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
    {
        await userRepository.DeleteUserAsync(id, cancellationToken);
    }
}