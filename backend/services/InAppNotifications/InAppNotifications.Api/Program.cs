using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.BsonClassMaps;
using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

BsonClassMapRegistry.RegisterBsonClassMaps();

builder.Services.AddMongoDataContextConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.RunAsync();
