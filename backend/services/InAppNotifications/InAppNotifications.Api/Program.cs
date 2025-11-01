using ch4rniauski.BankApp.InAppNotifications.Api.GraphQL.Queries;
using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.BsonClassMaps;
using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(opt =>
{
    opt.ValidateOnBuild = true;
    opt.ValidateScopes = true;
});

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<NotificationQuery>();

BsonClassMapRegistry.RegisterBsonClassMaps();

builder.Services.AddMongoDataContextConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGraphQL();

await app.RunAsync();
