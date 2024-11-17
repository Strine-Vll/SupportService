using Domain.Entities;

namespace Application.Authentication;

public interface IJwtProvider
{
    string Generate(User userAccount);
}
