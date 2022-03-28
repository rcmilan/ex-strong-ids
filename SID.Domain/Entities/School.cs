using SID.Domain.Base;

namespace SID.Domain.Entities
{
    public record SchoolId(Guid Value);

    public class School : BaseEntity<SchoolId>
    {
        public School()
        {

        }

        [System.Text.Json.Serialization.JsonConstructor()]
        public School(Guid id)
        {
            this.Id = Id;
        }

        public string Name { get; set; }
        public string Address { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}
