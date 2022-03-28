using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SID.Domain.Entities;

namespace SID.Infra.Configurations
{
    internal class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, value => new CourseId(value));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title);
            builder.Property(x => x.Classroom);
        }
    }
}