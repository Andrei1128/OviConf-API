using Microsoft.Extensions.DependencyInjection;
using PERSISTANCE.Contracts;
using PERSISTANCE.Implementations;

namespace PERSISTANCE.ServicesConfiguration;

public static class ServicesConfiguration
{
    public static void AddPersistanceServices(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<IAuthRepository, AuthRepository>();
    }
}
