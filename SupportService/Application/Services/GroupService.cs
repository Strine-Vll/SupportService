using Application.Abstractions;
using Application.Dtos.GroupDtos;
using Application.Dtos.UserDtos;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;

    private readonly IUserRepository _userRepository;

    private IMapper _mapper;

    public GroupService(IGroupRepository groupRepository, IUserRepository userRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<GroupDto>> GetUserGroups(int userId)
    {
        var dbGroups = await _groupRepository.GetUserGroupsAsync(userId);

        var viewGroups = _mapper.Map<List<GroupDto>>(dbGroups);

        return viewGroups;
    }

    public async Task<GroupDto> GetGroupById(int groupId)
    {
        var dbGroup = await _groupRepository.GetByIdAsync(groupId);

        var viewGroup = _mapper.Map<GroupDto>(dbGroup);

        return viewGroup;
    }

    public async Task UpdateUsers(int groupId, List<UserPreviewDto> users)
    {
        var dbGroup = await _groupRepository.GetByIdWithUsersAsync(groupId);

        var userIds = users.Select(u => u.Id).ToList();

        var dbUsers = await _userRepository.GetByIdsAsync(userIds);

        dbGroup.Users.RemoveAll(u => !userIds.Contains(u.Id));

        foreach (var user in dbUsers)
        {
            if (!dbGroup.Users.Any(u => u.Id == user.Id))
            {
                dbGroup.Users.Add(user);
            }
        }

        await _groupRepository.UpdateAsync(dbGroup);
    }

    public async Task CreateGroup(GroupDto group, int userId)
    {
        var dbGroup = _mapper.Map<Group>(group);

        var user = await _userRepository.GetByIdAsync(userId);

        dbGroup.Users.Add(user);

        await _groupRepository.CreateAsync(dbGroup);
    }

    public async Task InviteUser(int groupId, int userId)
    {
        var group = await _groupRepository.GetGroupByIdWithUsers(groupId);

        var user = await _userRepository.GetByIdAsync(userId);

        group.Users.Add(user);

        await _groupRepository.UpdateAsync(group);
    }

    public async Task RemoveUserFromGroup(int groupId, int userId)
    {
        var group = await _groupRepository.GetGroupByIdWithUsers(groupId);

        var user = await _userRepository.GetByIdAsync(userId);

        group.Users.Remove(user);

        await _groupRepository.UpdateAsync(group);
    }

    public async Task UpdateGroup(GroupDto group)
    {
        var dbGroup = await _groupRepository.GetByIdAsync(group.Id);

        dbGroup.Name = group.Name;

        await _groupRepository.UpdateAsync(dbGroup);
    }

    public async Task DeleteGroup(int groupId)
    {
        await _groupRepository.DeleteByIdAsync(groupId);
    }
}
