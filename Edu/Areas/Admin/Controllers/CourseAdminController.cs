using Edu.DAL;
using Edu.Models;
using Edu.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edu.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseAdminController : Controller
    {
        public readonly AppDbContext _context;
        public readonly ICourseService _service;
        public CourseAdminController(AppDbContext context, ICourseService service)
        {
            _context = context;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Where(c=>!c.IsDeleted).Include(c=>c.CourseImgs).Include(c=>c.Author).ToListAsync();
            return View(courses);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var course = await _service.GetById(id);
            if (course == null) return NotFound();

            _context.Remove(course);

            await _context.SaveChangesAsync();

            return Ok();

        }
        [HttpPost]
        public async Task<IActionResult> Archive(int? id)
        {
            if (id == null) return BadRequest();
            var course = await _service.GetById(id);
            if (course == null) return NotFound();


            course.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok();

        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            var course = await _service.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            var singleCourse = await _context.Courses.Where(c => !c.IsDeleted).Include(c => c.CourseImgs).Include(c => c.Author).FirstOrDefaultAsync(m=>m.Id==id);

            var CourseImages = singleCourse.CourseImgs.Where(c => !c.IsDeleted).ToList();
            ViewBag.images = CourseImages;
            return View(course);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpGet]
        
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var course = await _service.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            var singleCourse = await _context.Courses.Where(c => !c.IsDeleted).Include(c => c.CourseImgs).Include(c => c.Author).FirstOrDefaultAsync(m => m.Id == id);
            var CourseImages = singleCourse.CourseImgs.Where(c => !c.IsDeleted).ToList();
            ViewBag.images = CourseImages;
            return View(course);
        }
    }
}
