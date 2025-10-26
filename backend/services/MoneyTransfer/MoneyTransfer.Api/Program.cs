using ch4rniauski.BankApp.MoneyTransfer.Application.Extensions;
using ch4rniauski.BankApp.MoneyTransfer.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(opt =>
{
    opt.ValidateOnBuild = true;
    opt.ValidateScopes = true;
});

builder.Services.AddSwaggerGen();

builder.Services.AddMoneyTransferConfiguration(builder.Configuration);
builder.Services.AddMediatrConfiguration();
builder.Services.AddFluentValidationConfiguration();
builder.Services.AddAutoMapperConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.ApplyMigrations();

await app.RunAsync();
