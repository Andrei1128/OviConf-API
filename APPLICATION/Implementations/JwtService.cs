using APPLICATION.Contracts;
using DOMAIN.DTOs;
using DOMAIN.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APPLICATION.Implementations;

public class JwtService : IJwtService
{
    private readonly AppSettings _appSettings;
    public JwtService(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }
    public string GenerateToken(UserDTO user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_appSettings.JwtSettings.Key);
        var expiresIn = Convert.ToDouble(_appSettings.JwtSettings.ExpiresInHours);

        var claims = new List<Claim>()
        {
            new("Id",user.Id.ToString()),
            new("Name",user.Name),
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
            Issuer = _appSettings.JwtSettings.Issuer,
            Audience = _appSettings.JwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
