using APPLICATION.Contracts;
using APPLICATION.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace APPLICATION.ServicesConfiguration;

public static class AppServicesConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IRoleService, RoleService>();
    }
}
