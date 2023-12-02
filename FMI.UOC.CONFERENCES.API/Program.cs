using API.ServicesConfiguration;
using APPLICATION.ServicesConfiguration;
using PERSISTANCE.ServicesConfiguration;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddCustomAuthentication(config);
builder.Services.AddCustomAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddPersistanceServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
