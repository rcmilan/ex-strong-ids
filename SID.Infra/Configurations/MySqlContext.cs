using Microsoft.EntityFrameworkCore;
using SID.Domain.Entities;

namespace SID.Infra.Configurations
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
