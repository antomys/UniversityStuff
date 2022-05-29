using System.Reflection;
using gRPC.Server.Services;
using gRPC.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Server.Extensions;

public static class DatabaseExtensions
{
    private const string RelationalDb = nameof(RelationalDb);
    
    public static IServiceCollection AddRelationalDatabase(this IServiceCollection service, IConfiguration configuration)
    {
        service
            .AddTransient<IProductService, ProductService>();
        
        return service.AddDbContext<ServerContext>(
            builder => builder.UseSqlite
            (configuration.GetConnectionString(RelationalDb),
                optionsBuilder => optionsBuilder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)), 
            ServiceLifetime.Transient);
    }
}