using Application.Abstractions;
using Application.Dtos.GroupDtos;
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

        var viewGoals = _mapper.Map<List<GroupDto>>(dbGroups);

        return viewGoals;
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
        var group = await _groupRepository.GetByIdAsync(groupId);

        var user = await _userRepository.GetByIdAsync(userId);

        group.Users.Add(user);

        await _groupRepository.UpdateAsync(group);
    }

    public async Task RemoveUserFromGroup(int groupId, int userId)
    {
        var group = await _groupRepository.GetByIdAsync(groupId);

        var user = await _userRepository.GetByIdAsync(userId);

        group.Users.Remove(user);

        await _groupRepository.UpdateAsync(group);
    }
}
