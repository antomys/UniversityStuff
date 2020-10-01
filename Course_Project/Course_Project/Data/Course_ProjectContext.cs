using Microsoft.EntityFrameworkCore;
using Course_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

/*namespace Course_Project.Data
{
    public class Course_ProjectContext : DbContext
    {
        public Course_ProjectContext(DbContextOptions<Course_ProjectContext> options) : base(options)
        {

        }

        public DbSet<Course_Project.Models.Student> Student { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>().ToTable("Student");

        }

    }
}*/


namespace Course_Project.Data
{
    public class Course_ProjectContext : IdentityDbContext<IdentityUser>
    {
        public Course_ProjectContext(DbContextOptions<Course_ProjectContext> options) : base(options)
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

        public DbSet<Course_Project.Models.Student> Student { get; set; }

        public DbSet<Course_Project.Models.StudentLabWork> StudentLabWork { get; set; }
    }

}
