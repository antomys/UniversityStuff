using gRPC.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Server;

public class ServerContext : DbContext
{
    public ServerContext(DbContextOptions<ServerContext> contextOptions)
        :base(contextOptions) { }
    
    public DbSet<ProductEntity> Products { get; set; }
}