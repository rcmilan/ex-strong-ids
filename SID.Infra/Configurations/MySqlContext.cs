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

            //var courses1 = new List<Course> {
            //    new Course { Title = "Curso123" },
            //    new Course { Title = "Curso456" }
            //};

            //var courses2 = new List<Course> {
            //    new Course { Title = "Curso789" },
            //    new Course { Title = "Curso000" }
            //};

            //modelBuilder.Entity<School>().HasData(
            //    new School { Name = "school 1", Courses = courses1 },
            //    new School { Name = "school 2", Courses = courses2 }
            //    );

            base.OnModelCreating(modelBuilder);
        }
    }
}
