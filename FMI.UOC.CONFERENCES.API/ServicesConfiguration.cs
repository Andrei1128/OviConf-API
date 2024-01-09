using API.Middlewares;
using DOMAIN.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Text;

namespace API.ServicesConfiguration;

public static partial class ServicesConfiguration
{
    public static void AddCors(this IServiceCollection service, string[] allowedOrigins)
    {
        service.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
            });
        });
    }
    public static void AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                    new string[]{}
                }
            });
        });
    }
    public static void AddMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ExceptionHandlerMiddleware>();
        services.AddScoped<JwtUnwrapperMiddleware>();
    }
    public static AppSettings BindAppSettings(this WebApplicationBuilder builder)
    {
        var appSettings = new AppSettings();
        builder.Configuration.GetSection(nameof(AppSettings)).Bind(appSettings);

        builder.Services.AddSingleton(appSettings);

        return appSettings;
    }
    public static Serilog.Core.Logger CreateLogger(string SqlServerConnectionString)
    {
        return new LoggerConfiguration()
             .WriteTo.MSSqlServer(SqlServerConnectionString, new MSSqlServerSinkOptions() { TableName = "tbl_Logs", AutoCreateSqlTable = true })
             .MinimumLevel.Information()
             .CreateLogger();
    }
    public static void AddCustomAuthentication(this IServiceCollection services, JwtSettings jwtSettings) =>
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new()
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
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
