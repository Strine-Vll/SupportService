﻿using Application.Dtos.GroupDtos;
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
}