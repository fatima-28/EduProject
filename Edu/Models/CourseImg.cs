using Edu.Models.Common;

namespace Edu.Models
{
    public class CourseImg:BaseEntity
    {
        public string? Url { get; set; }
        public int CourseId { get; set; }
        public bool IsItMain { get; set; } = false;
        public Course? Course { get; set; }
    }
}
