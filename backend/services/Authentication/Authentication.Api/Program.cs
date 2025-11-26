using ch4rniauski.BankApp.Authentication.Application.Extensions;
using ch4rniauski.BankApp.Authentication.Infrastructure.Extensions;
using ch4rniauski.BankApp.Authentication.Infrastructure.Services.gRPC;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(opt =>
{
    opt.ValidateOnBuild = true;
    opt.ValidateScopes = true;
});

builder.Services.AddControllers();
builder.Services.AddGrpc();

builder.Services.AddAuthenticationContextConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddValidationConfiguration();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddMediatrConfiguration();

var app = builder.Build();

app.MapControllers();
app.MapGrpcService<ClientsGrpcService>();

await app.ApplyMigrations();

await app.RunAsync();
