using DistEduDatabases2.Mongo.Services;
using DistEduDatabases2.Mongo.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DistEduDatabases2.Mongo.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDocumentedDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<MongoSettings>(configuration.GetSection(nameof(MongoSettings)));

        serviceCollection
            .AddSingleton<IDocumentedUserService, DocumentedUserService>()
            .AddSingleton<IDocumentedCvService, DocumentedCvService>();

        return serviceCollection;
    }
}