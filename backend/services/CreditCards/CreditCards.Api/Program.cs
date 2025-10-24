using ch4rniauski.BankApp.CreditCards.Application.Extensions.DependencyInjectionExtensions;
using ch4rniauski.BankApp.CreditCards.Infrastructure.Extensions.DependencyInjectionExtensions;
using ch4rniauski.BankApp.CreditCards.Infrastructure.Services.gRPC;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(opt =>
{
    opt.ValidateOnBuild = true;
    opt.ValidateScopes = true;
});

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddGrpc();

builder.Services.AddCreditCardContext(builder.Configuration);
builder.Services.AddHashConfiguration(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddSensitiveDataConfiguration();
builder.Services.AddMediatrConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGrpcService<CreditCardsGrpcService>();

app.Run();
