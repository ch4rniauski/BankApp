using ch4rniauski.BankApp.CreditCards.Application.Extensions.DependencyInjectionExtensions;
using ch4rniauski.BankApp.CreditCards.Infrastructure.Extensions.DependencyInjectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddCreditCardContext(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddSensitiveDataConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
