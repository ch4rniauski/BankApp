using ch4rniauski.BankApp.CreditCards.Application.Extensions;
using ch4rniauski.BankApp.CreditCards.Infrastructure.Extensions;
using ch4rniauski.BankApp.CreditCards.Infrastructure.Services.gRPC;

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

builder.Services.AddCreditCardContext(builder.Configuration);
builder.Services.AddHashConfiguration(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddSensitiveDataConfiguration();
builder.Services.AddMediatrConfiguration();

var app = builder.Build();

app.UseCors();

app.MapControllers();
app.MapGrpcService<CreditCardsGrpcService>();

await app.ApplyMigrations();

await app.RunAsync();
