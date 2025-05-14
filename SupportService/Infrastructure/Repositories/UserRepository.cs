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
            .Where(u => u.Groups.Any(g => g.Id == groupId))
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

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await _dbContext.Users.AnyAsync(u => u.Email == email);
    }
}
