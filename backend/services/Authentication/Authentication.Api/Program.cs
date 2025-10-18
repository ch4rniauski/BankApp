using ch4rniauski.BankApp.Authentication.Application.Extensions.DependencyInjectionExtensions;
using ch4rniauski.BankApp.Authentication.Infrastructure.Extensions.DependencyInjectionExtensions;
using ch4rniauski.BankApp.Authentication.Infrastructure.Services.gRPC;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(opt =>
{
    opt.ValidateOnBuild = true;
    opt.ValidateScopes = true;
});

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddGrpc();

builder.Services.AddAuthenticationContextConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddValidationConfiguration();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddMediatrConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<ClientsGrpcService>();

app.Run();
