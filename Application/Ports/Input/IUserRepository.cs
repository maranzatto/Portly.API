using Portly.Domain.Entities;

namespace Portly.Application.Ports.Input;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    Task AddAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task UpdateAsync(User user);
}
