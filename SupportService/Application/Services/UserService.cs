using Application.Abstractions;
using Application.Authentication;
using Application.Dtos.UserDtos;
using Application.Exceptions;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _user;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly IValidator<RegisterDto> _registerDtoValidator;
    private readonly IValidator<AuthenticationRequest> _authenticationRequestValidator;

    public UserService(IUserRepository user, IJwtProvider jwtProvider, IMapper mapper,
        IValidator<RegisterDto> registerDtoValidator, IValidator<AuthenticationRequest> authenticationRequestValidator)
    {
        _user = user;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _registerDtoValidator = registerDtoValidator;
        _authenticationRequestValidator = authenticationRequestValidator;
    }

    public async Task<List<UserPreviewDto>> GetGroupUsersAsync(int groupId)
    {
        var dbUsers = await _user.GetGroupUsersAsync(groupId);

        var viewUsers = _mapper.Map<List<UserPreviewDto>>(dbUsers);

        return viewUsers;
    }

    public async Task<List<UserPreviewDto>> GetUsersToInviteAsync(int groupId)
    {
        var dbUsers = await _user.GetUsersToInviteAsync(groupId);

        var viewUsers = _mapper.Map<List<UserPreviewDto>>(dbUsers);

        return viewUsers;
    }

    public async Task<List<UserPreviewDto>> GetUsersToManage()
    {
        var dbUsers = await _user.GetAllAsync();

        var viewUsers = _mapper.Map<List<UserPreviewDto>>(dbUsers);

        return viewUsers;
    }

    public async Task<List<UserPreviewDto>> GetActiveUsers()
    {
        var dbUsers = await _user.GetActiveUsers();

        var viewUsers = _mapper.Map<List<UserPreviewDto>>(dbUsers);

        return viewUsers;
    }

    public async Task<EditUserDto> GetEditUser(int userId)
    {
        var dbUser = await _user.GetByIdWithRole(userId);

        var viewUser = _mapper.Map<EditUserDto>(dbUser);

        return viewUser;
    }

    public async Task RegisterAsync(RegisterDto userToRegistrate)
    {
        var validationResult = await _registerDtoValidator.ValidateAsync(userToRegistrate);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = _mapper.Map<User>(userToRegistrate);

        string securePassword = CreatePassword(user.Password, out byte[] passwordSalt);

        user.Password = securePassword;
        user.Salt = passwordSalt;
        user.RoleId = (int)UserRoleEnum.User;

        await _user.CreateAsync(user);
    }

    public async Task<string> Authenticate(AuthenticationRequest request)
    {
        var validationResult = _authenticationRequestValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = await _user.GetUserByEmailAsync(request.Email);

        if (user == null || user.IsDeactivated)
        {
            throw new InvalidCredentialsException();
        }
        if (!VerifyPasswordHash(request.Password, user.Password, user.Salt))
        {
            throw new InvalidCredentialsException();
        }

        var token = _jwtProvider.Generate(user);

        return token;
    }

    public string CreatePassword(string password, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            password = Encoding.UTF8.GetString(passwordHash);

            return password;
        }
    }

    public bool VerifyPasswordHash(string password, string passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var translatedHash = Encoding.UTF8.GetString(computedHash);

            return translatedHash.Equals(passwordHash);
        }
    }

    public async Task UpdateUser(EditUserDto user)
    {
        var dbUser = await _user.GetByIdWithRole(user.Id);

        dbUser.Name = user.Name;
        dbUser.Email = user.Email;
        dbUser.RoleId = user.Role.Id;

        await _user.UpdateAsync(dbUser);
    }

    public async Task DeactivateUser(int userId)
    {
        var user = await _user.GetByIdAsync(userId);

        if (user != null)
        {
            user.IsDeactivated = !user.IsDeactivated;

            await _user.UpdateAsync(user);
        }
    }
}
