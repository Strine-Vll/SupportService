﻿using Application.Dtos.UserDtos;
using Domain.Entities;

namespace Application.Abstractions;

public interface IUserService
{
    Task<List<UserPreviewDto>> GetGroupUsersAsync(int groupId);

    Task RegisterAsync(RegisterDto userToRegistrate);

    Task<string> Authenticate(AuthenticationRequest request);

    string CreatePassword(string password, out byte[] passwordSalt);

    bool VerifyPasswordHash(string password, string passwordHash, byte[] passwordSalt);
}
