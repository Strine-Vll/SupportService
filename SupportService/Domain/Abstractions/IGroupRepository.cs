using Domain.Entities;

namespace Domain.Abstractions;

public interface IGroupRepository : IBaseRepository<Group>
{
    Task<List<Group>> GetUserGroupsAsync(int userId);

    Task<Group> GetByIdWithUsersAsync(int groupId);

    Task<Group> GetGroupByIdWithUsers(int groupId);
}
