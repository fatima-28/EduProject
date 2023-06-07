using Edu.Models.Common;

namespace Edu.Models
{
    public class Author:BaseEntity
    {
        public string? FullName { get; set; }
        public string? Image { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
