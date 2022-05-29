using System.Reflection;
using DistEduDatabases2.Relational.Context;
using DistEduDatabases2.Relational.Services;
using DistEduDatabases2.Relational.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DistEduDatabases2.Relational.Extensions;

public static class ServiceCollectionExtensions
{
    private const string RelationalDb = nameof(RelationalDb);

    public static IServiceCollection AddRelationalDatabase(this IServiceCollection service, IConfiguration configuration)
    {
        service
            .AddTransient<IRelationUserService, RelationUserService>()
            .AddTransient<IRelationCvService, RelationCvService>();
        
        return service.AddDbContext<ServerContext>(
            builder => builder.UseSqlite
            (configuration.GetConnectionString(RelationalDb),
                optionsBuilder => optionsBuilder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)), 
            ServiceLifetime.Transient);
    }
}