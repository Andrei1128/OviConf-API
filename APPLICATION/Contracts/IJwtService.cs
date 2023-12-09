using DOMAIN.DTOs;

namespace APPLICATION.Contracts;

public interface IJwtService
{
    string GenerateToken(UserDTO user);
}
