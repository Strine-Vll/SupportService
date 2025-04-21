using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
    Task<List<User>> GetGroupUsersAsync(int groupId);

    Task<User> GetUserByEmailAsync(string email);

    Task<bool> IsEmailUniqueAsync(string email);
}
