using Edu.DAL;
using Edu.Models;
using Edu.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Edu.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class ArchiveController : Controller
    {
        private AppDbContext _context { get; }
        private IEnumerable<Course> _courses;
        private readonly ICourseService _courseservice;
        public ArchiveController(ICourseService courseService, AppDbContext context)
        {
            _context = context;
            _courseservice = courseService;
           

        }
        public IActionResult GetArchivedCourses()
        {
            var courses = _context.Courses.Include(m => m.CourseImgs).Include(c=>c.Author).Where(m => m.IsDeleted).ToList();

            return View(courses);
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null) return BadRequest();

            Course course = await _context.Courses.Where(m => m.IsDeleted == true).FirstOrDefaultAsync(m => m.Id == id);
            if (course == null) return NotFound();


            course.IsDeleted = false;
            await _context.SaveChangesAsync();

            return Ok();



        }
    }
}
