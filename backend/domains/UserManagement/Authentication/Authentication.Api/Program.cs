using ch4rniauski.BankApp.Authentication.Application.Extensions.DependencyInjectionExtensions;
using ch4rniauski.BankApp.Authentication.Infrastructure.Extensions.DependencyInjectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddAuthenticationContextConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
