using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SID.Domain.Entities;

namespace SID.Infra.Configurations
{
    internal class SchoolEntityTypeConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder
                .Property(s => s.Id)
                .HasConversion(id => id.Value, value => new SchoolId(value));
        }
    }
}
