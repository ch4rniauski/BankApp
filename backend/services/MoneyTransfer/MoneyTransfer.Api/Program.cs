using ch4rniauski.BankApp.MoneyTransfer.Application.Extensions;
using ch4rniauski.BankApp.MoneyTransfer.Infrastructure.Extensions;

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

builder.Services.AddMoneyTransferConfiguration(builder.Configuration);
builder.Services.AddMediatrConfiguration();
builder.Services.AddFluentValidationConfiguration();
builder.Services.AddAutoMapperConfiguration();

var app = builder.Build();

app.UseCors();

app.MapControllers();

await app.ApplyMigrations();

await app.RunAsync();
