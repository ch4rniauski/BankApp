using ch4rniauski.BankApp.InAppNotifications.Api.GraphQL.Queries;
using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.BsonClassMaps;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<NotificationQuery>();

BsonClassMapRegistry.RegisterBsonClassMaps();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGraphQL();

await app.RunAsync();
