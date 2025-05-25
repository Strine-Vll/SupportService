using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetGroupUsersAsync(int groupId)
    {
        var result = await _dbContext.Users
            .Where(u => u.Groups.Any(g => g.Id == groupId) 
                && u.IsDeactivated == false)
            .ToListAsync();

        return result;
    }

    public async Task<List<User>> GetUsersToInviteAsync(int groupId)
    {
        var result = await _dbContext.Users
            .Where(u => u.IsDeactivated == false
                && !u.Groups.Any(g => g.Id == groupId)
                && (u.RoleId == 2 || u.RoleId == 3))
            .ToListAsync();

        return result;
    }

    public async Task<List<User>> GetByIdsAsync(List<int> userIds)
    {
        var result = await _dbContext.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();

        return result;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var result = await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);

        return result;
    }

    public async Task<User> GetByIdWithRole(int userId)
    {
        var result = await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == userId);

        return result;
    }

    public async Task<List<User>> GetActiveUsers()
    {
        var result = await _dbContext.Users
            .Where(u => u.IsDeactivated == false)
            .ToListAsync();

        return result;
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await _dbContext.Users.AnyAsync(u => u.Email == email);
    }
}
