using Domain.Entities;

namespace Domain.Abstractions;

public interface IGroupRepository : IBaseRepository<Group>
{
    Task<List<Group>> GetUserGroupsAsync(int userId);

    Task<Group> GetGroupByIdWithUsers(int groupId);
}
