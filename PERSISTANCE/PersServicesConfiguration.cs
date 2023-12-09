using Microsoft.Extensions.DependencyInjection;
using PERSISTANCE.Contracts;
using PERSISTANCE.Implementations;

namespace PERSISTANCE.ServicesConfiguration;

public static class PersServicesConfiguration
{
    public static void AddPersistanceServices(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    }
}
