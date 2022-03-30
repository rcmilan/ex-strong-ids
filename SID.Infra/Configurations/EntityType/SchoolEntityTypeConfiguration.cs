using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SID.Domain.Entities;

namespace SID.Infra.Configurations.EntityType
{
    internal class SchoolEntityTypeConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, value => new SchoolId(value));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name);
            builder.Property(x => x.Address);

            builder
                .HasMany(s => s.Courses)
                .WithOne(c => c.School);
        }
    }
}