using DOMAIN.DTOs;

namespace DOMAIN.Responses;

public class AuthResponse
{
    public UserWithRolesDTO User { get; set; }
    public string Jwt { get; set; } = string.Empty;
}
