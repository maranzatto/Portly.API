using Microsoft.EntityFrameworkCore;
using Portly.Application.Ports.Input;
using Portly.Domain.Entities;
using Portly.Infrastructure.Data;

namespace Portly.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PortlyDbContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(PortlyDbContext context)
    {
        _context = context;
        _users = context.Set<User>();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateAsync(User user)
    {
        _users.Update(user);
        await _context.SaveChangesAsync();
    }
}
