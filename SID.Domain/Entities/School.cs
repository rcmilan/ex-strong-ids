using SID.Domain.Base;

namespace SID.Domain.Entities
{
    public record SchoolId(Guid Value);

    public class School : BaseEntity<SchoolId>
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}
