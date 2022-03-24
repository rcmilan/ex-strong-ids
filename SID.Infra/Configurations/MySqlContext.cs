using Microsoft.EntityFrameworkCore;
using SID.Domain.Entities;

namespace SID.Infra.Configurations
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SchoolEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CourseEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
