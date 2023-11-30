using DOMAIN.Models;

namespace APPLICATION.Contracts;

public interface IJwtService
{
    string GenerateToken(User user);
}
