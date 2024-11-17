using Application.Dtos.UserDtos;

namespace Application.Abstractions;

public interface IUserService
{
    Task RegisterAsync(RegisterDto userToRegistrate);

    Task<string> Authenticate(AuthenticationRequest request);

    string CreatePassword(string password, out byte[] passwordSalt);

    bool VerifyPasswordHash(string password, string passwordHash, byte[] passwordSalt);
}
