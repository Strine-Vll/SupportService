using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
    Task<List<User>> GetGroupUsersAsync(int groupId);

    Task<List<User>> GetByIdsAsync(List<int> userIds);

    Task<List<User>> GetUsersToInviteAsync(int groupId);

    Task<User> GetByIdWithRole(int userId);

    Task<User> GetUserByEmailAsync(string email);

    Task<List<User>> GetActiveUsers();

    Task<bool> IsEmailUniqueAsync(string email);
}
