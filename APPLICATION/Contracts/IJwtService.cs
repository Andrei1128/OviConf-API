using DOMAIN.DTOs;

namespace APPLICATION.Contracts;

public interface IJwtService
{
    string GenerateToken(UserWithRolesDTO user);
}
