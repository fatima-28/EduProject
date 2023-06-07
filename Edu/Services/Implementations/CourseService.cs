using Edu.DAL;
using Edu.Models;
using Edu.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Edu.Services.Implementations
{
    public class CourseService:ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<IEnumerable<Course>> GetAll()
        { 
            var res= await _context.Courses.Include(m => m.CourseImgs).Include(m => m.Author).Where(m => !m.IsDeleted).Take(3).ToListAsync();

            return res;
        }


        public async Task<Course> GetById(int? id)
        {
            var res= await _context.Courses.FindAsync(id);
            return res;
        }

      
    }
}
