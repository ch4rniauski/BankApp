using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.BsonClassMaps;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

BsonClassMapRegistry.RegisterBsonClassMaps();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.RunAsync();
