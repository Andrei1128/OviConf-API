using APPLICATION.Contracts;
using DOMAIN.DTOs;
using DOMAIN.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APPLICATION.Implementations;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    private readonly AppSettings _appSettings;
    public JwtService(IConfiguration config, IOptions<AppSettings> options)
    {
        _config = config;
        _appSettings = options.Value;
    }
    public string GenerateToken(UserDTO user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_appSettings.JwtSettings.Key);
        var expiresIn = Convert.ToDouble(_appSettings.JwtSettings.ExpiresInHours);

        var claims = new List<Claim>()
        {
            new("Id",user.Id.ToString()),
            new("Email",user.Email),
        };

        IEnumerable<string> disctinctRoles = user.Roles.Select(role => role.RoleName).Distinct();

        foreach (var role in disctinctRoles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var speakerConferenceIdsList = user.Roles
                .Where(role => role.RoleName == IdentityData.Speaker)
                .Select(role => role.ConferenceId);

        string speakerConferenceIds = string.Join(",", speakerConferenceIdsList);
        claims.Add(new Claim(ConferenceIdsClaim.Speaker, speakerConferenceIds));

        var managerConferenceIdsList = user.Roles
                .Where(role => role.RoleName == IdentityData.Manager)
                .Select(role => role.ConferenceId);

        string managerConferenceIds = string.Join(",", managerConferenceIdsList);
        claims.Add(new Claim(ConferenceIdsClaim.Manager, managerConferenceIds));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(expiresIn),
            Issuer = _config.GetSection(_appSettings.JwtSettings.Issuer).Value,
            Audience = _config.GetSection(_appSettings.JwtSettings.Audience).Value,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
