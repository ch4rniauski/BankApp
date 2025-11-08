using ch4rniauski.BankApp.Authentication.Application.Extensions;
using ch4rniauski.BankApp.Authentication.Infrastructure.Extensions;
using ch4rniauski.BankApp.Authentication.Infrastructure.Services.gRPC;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(opt =>
{
    opt.ValidateOnBuild = true;
    opt.ValidateScopes = true;
});

builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
    }));

builder.Services.AddControllers();
builder.Services.AddGrpc();

builder.Services.AddAuthenticationContextConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddValidationConfiguration();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddMediatrConfiguration();

var app = builder.Build();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<ClientsGrpcService>();

await app.ApplyMigrations();

await app.RunAsync();
