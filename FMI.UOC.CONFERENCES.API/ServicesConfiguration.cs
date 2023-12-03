using DOMAIN.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.ServicesConfiguration;

public static class ServicesConfiguration
{
    public static void AddCustomAuthentication(this IServiceCollection services, ConfigurationManager config) =>
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new()
                {
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

    public static void AddCustomAuthorization(this IServiceCollection services) =>
        services.AddAuthorization(options =>
        {
            options.AddPolicy(IdentityData.Admin, p => p.RequireRole(IdentityData.Admin));
            options.AddPolicy(IdentityData.Helper, p => p.RequireRole(IdentityData.Helper, IdentityData.Admin));
            options.AddPolicy(IdentityData.Manager, p => p.RequireRole(IdentityData.Manager, IdentityData.Helper, IdentityData.Admin));
            options.AddPolicy(IdentityData.Speaker, p => p.RequireRole(IdentityData.Speaker, IdentityData.Manager, IdentityData.Helper, IdentityData.Admin));
            options.AddPolicy(IdentityData.User, p => p.RequireRole(IdentityData.User, IdentityData.Speaker, IdentityData.Manager, IdentityData.Helper, IdentityData.Admin));
        });
}
