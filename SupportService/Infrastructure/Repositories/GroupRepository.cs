using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    private readonly ApplicationDbContext _dbContext;

    public GroupRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Group>> GetUserGroupsAsync(int userId)
    {
        var result = await _dbContext.Groups
            .Where(g => g.Users.Any(u => u.Id == userId))
            .ToListAsync();

        return result;
    }
}
