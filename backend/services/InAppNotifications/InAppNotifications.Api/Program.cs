using ch4rniauski.BankApp.InAppNotifications.Api.Extensions;
using ch4rniauski.BankApp.InAppNotifications.Api.GraphQL.Queries;
using ch4rniauski.BankApp.InAppNotifications.Application.Extensions;
using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.BsonClassMaps;
using ch4rniauski.BankApp.InAppNotifications.Infrastructure.MongoDb.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(opt =>
{
    opt.ValidateOnBuild = true;
    opt.ValidateScopes = true;
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<NotificationQuery>();

BsonClassMapRegistry.RegisterBsonClassMaps();

builder.Services.AddMongoDataContextConfiguration(builder.Configuration);
builder.Services.AddRabbitMqConfiguration(builder.Configuration);
builder.Services.AddMediatrConfiguration();
builder.Services.AddAutoMapperConfiguration();

var app = builder.Build();

app.MapGraphQL();

await app.RunAsync();
