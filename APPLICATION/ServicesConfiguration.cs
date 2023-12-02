﻿using APPLICATION.Contracts;
using APPLICATION.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace APPLICATION.ServicesConfiguration;

public static class ServicesConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
    }
}