using Edu.Models.Common;

namespace Edu.Models
{
    public class Course:BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Hour { get; set; }
        public double Price { get; set; }
        public int Capacity { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
       
        public ICollection<CourseImg>? CourseImgs { get; set; }
      
        
    }
}
