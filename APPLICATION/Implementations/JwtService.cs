using APPLICATION.Contracts;
using DOMAIN.Models;
using DOMAIN.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APPLICATION.Implementations;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    public JwtService(IConfiguration config) => _config = config;
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings:Key").Value!);
        var expiresIn = Convert.ToDouble(_config.GetSection("JwtSettings:ExpireIn").Value);

        var claims = new List<Claim>()
        {
            new("Id",user.Id.ToString()),
            new("Email",user.Email),
        };

        IEnumerable<string> disctincRoles = user.Roles.Select(role => role.Value).Distinct();

        foreach (var role in disctincRoles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var speakerConferenceIdsList = user.Roles
                .Where(role => role.Value == IdentityData.Speaker)
                .Select(role => role.ConferenceId);

        string speakerConferenceIds = string.Join(",", speakerConferenceIdsList);
        claims.Add(new Claim(ConferenceIdsClaim.Speaker, speakerConferenceIds));

        var managerConferenceIdsList = user.Roles
                .Where(role => role.Value == IdentityData.Manager)
                .Select(role => role.ConferenceId);

        string managerConferenceIds = string.Join(",", managerConferenceIdsList);
        claims.Add(new Claim(ConferenceIdsClaim.Manager, managerConferenceIds));

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
