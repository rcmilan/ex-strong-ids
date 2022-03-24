using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SID.Domain.Entities;

namespace SID.Infra.Configurations
{
    internal class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .Property(s => s.Id)
                .HasConversion(id => id.Value, value => new CourseId(value));
        }
    }
}
