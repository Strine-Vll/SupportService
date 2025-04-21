using Application.Dtos.GroupDtos;
using Application.Dtos.UserDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IGroupService
{
    Task<List<GroupDto>> GetUserGroups(int userId);

    Task CreateGroup(GroupDto group, int userId);

    Task InviteUser(int groupId, int userId);

    Task RemoveUserFromGroup(int groupId, int userId);
}
