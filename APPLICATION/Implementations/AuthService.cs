using DOMAIN.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APPLICATION.Implementations;

public class AuthService
{
    private readonly IConfiguration _config;
    public AuthService(IConfiguration config)
    {
        _config = config;
    }
    private string GenerateToken(TokenGenerationRequest payload)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings:Key").Value!);
        var expiresIn = Convert.ToDouble(_config.GetSection("JwtSettings:ExpireIn").Value);

        var claims = new List<Claim>()
        {
            new("asd","asd"),
            new(ClaimTypes.Role,"asd")
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expiresIn),
            Issuer = _config.GetSection("JwtSettings:Issuer").Value,
            Audience = _config.GetSection("JwtSettings:Audience").Value,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var jwt = tokenHandler.WriteToken(token);
        return jwt;
    }
}
