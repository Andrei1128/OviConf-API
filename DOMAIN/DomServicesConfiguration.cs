using DOMAIN.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace DOMAIN.ServicesConfiguration;

public static class DomServicesConfiguration
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ThisUser>();
    }
}
