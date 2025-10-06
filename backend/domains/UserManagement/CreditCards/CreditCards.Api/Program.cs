using ch4rniauski.BankApp.CreditCards.Infrastructure.Extensions.DependencyInjectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddCreditCardContext(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
