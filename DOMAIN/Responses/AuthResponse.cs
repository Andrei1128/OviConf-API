using DOMAIN.Models;

namespace DOMAIN.Responses;

public class AuthResponse
{
    public User User { get; set; }
    public string Jwt { get; set; } = string.Empty;
}
