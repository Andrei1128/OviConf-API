using DOMAIN.DTOs;

namespace DOMAIN.Responses;

public class AuthResponse
{
    public UserDTO User { get; set; }
    public string Jwt { get; set; } = string.Empty;
}
