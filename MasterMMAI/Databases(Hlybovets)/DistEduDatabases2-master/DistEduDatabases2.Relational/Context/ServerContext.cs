using DistEduDatabases2.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace DistEduDatabases2.Relational.Context;

public class ServerContext : DbContext
{
    public ServerContext(DbContextOptions<ServerContext> contextOptions)
        :base(contextOptions) { }
    
    public DbSet<CV> Cvs { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Language> Languages { get; set; }
    
    public DbSet<Passion> Passions { get; set; }
}