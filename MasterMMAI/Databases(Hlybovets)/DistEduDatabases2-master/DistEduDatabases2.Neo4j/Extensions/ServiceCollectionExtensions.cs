using DistEduDatabases2.Neo4j.Options;
using DistEduDatabases2.Neo4j.Services;
using DistEduDatabases2.Neo4j.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neo4jClient;

namespace DistEduDatabases2.Neo4j.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNeo4JDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<Neo4JOptions>(configuration.GetSection(nameof(Neo4JOptions)));

        var neo4JOptions = configuration.Get<Neo4JOptions>();
        
        var client = new BoltGraphClient(new Uri(neo4JOptions.Neo4J), neo4JOptions.DbName, neo4JOptions.Password);
        
        client.ConnectAsync();
        serviceCollection.AddSingleton<IGraphClient>(client);

        serviceCollection
            .AddTransient<IGraphUserService, GraphUserService>()
            .AddTransient<IGraphCvService, GraphCvService>();
        
        return serviceCollection;
    }
}