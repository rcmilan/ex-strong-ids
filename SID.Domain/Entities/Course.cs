using SID.Domain.Base;

namespace SID.Domain.Entities
{
    public record CourseId(int Value) : StronglyTypedId<int>(Value);

    public class Course : BaseEntity<CourseId>
    {
        public string Title { get; set; }
        public int Classroom { get; set; }

        public School School { get; set; }
    }
}