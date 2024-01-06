using API.Middlewares;
using API.ServicesConfiguration;
using APPLICATION.ServicesConfiguration;
using DOMAIN.ServicesConfiguration;
using PERSISTANCE.ServicesConfiguration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.BindAppSettings();

Log.Logger = API.ServicesConfiguration.ServicesConfiguration.CreateLogger(appSettings.DBConnections.SqlServer);

builder.Services.AddCustomAuthentication(appSettings.JwtSettings);
builder.Services.AddCustomAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();

builder.Services.AddMiddlewares();

builder.Services.AddApplicationServices();
builder.Services.AddDomainServices();
builder.Services.AddPersistanceServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<JwtUnwrapperMiddleware>();

app.MapControllers();

app.Run();
