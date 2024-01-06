using DOMAIN.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace DOMAIN.ServicesConfiguration;

public static partial class ServicesConfiguration
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ThisUser>();
    }
}
