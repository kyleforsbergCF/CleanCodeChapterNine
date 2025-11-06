using ChapterNine.Application.Models;

namespace ChapterNine.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken cancellationToken);
    Task<UserDto> CreateUserAsync(UserDto user, CancellationToken cancellationToken);
    Task<UserDto> UpdateUserAsync(UserDto user, CancellationToken cancellationToken);
    Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);
}