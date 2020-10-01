using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuppliersProducts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SuppliersProducts.Data
{
    public class DataBaseContext: IdentityDbContext<IdentityUser>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<LabWork> LabWorks { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Passing> Passings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LabWork>().ToTable("LabWork");
            modelBuilder.Entity<Teacher>().ToTable("Teacher");
            modelBuilder.Entity<Passing>().ToTable("Passing");
        }

        public DbSet<SuppliersProducts.Models.Student> Student { get; set; }

        public DbSet<SuppliersProducts.Models.StudentLabWork> StudentLabWork { get; set; }
    }

}
