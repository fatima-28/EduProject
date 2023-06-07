using Edu.DAL;
using Edu.Models;
using Edu.Services.Interfaces;
using Edu.ViewModels;
using Edu.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Edu.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICourseService _courseService;
        private readonly IHttpContextAccessor _accessor;
        public CourseController(AppDbContext context, ICourseService courseService, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            var count = await _context.Courses.Where(m => !m.IsDeleted).CountAsync();

            ViewBag.Count = count;

            CourseVM course = new()
            {
                Courses = await _courseService.GetAll()

        };
            return View(course);
        }
        [HttpPost]
       
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Course course = await _courseService.GetById(id);

            if (course is null) return NotFound();

            List<BasketVM> basket = GetBasketDatas();

            AddcourseToBasket(basket, course);

            return RedirectToAction("Index", "Course");
        }

        private List<BasketVM> GetBasketDatas()
        {
            List<BasketVM> basket;
            var basketSession = _accessor.HttpContext.Session.GetString("basket");

            if (basketSession != null)
            {
                basket = JsonSerializer.Deserialize<List<BasketVM>>(basketSession);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }

        private void AddcourseToBasket(List<BasketVM> basket, Course course)
        {
            BasketVM existcourse = basket.FirstOrDefault(m => m.Id == course.Id);

            if (existcourse is null)
            {
                basket.Add(new BasketVM
                {
                    Id = course.Id,
                    Count = 1
                });
            }
            else
            {
                existcourse.Count++;
            }

            _accessor.HttpContext.Session.SetString("basket", JsonSerializer.Serialize(basket));
        }
        public async Task<IActionResult> LoadMore(int skip)
        {
            IEnumerable<Course> courses = await _context.Courses.Include(m => m.CourseImgs).Include(c=>c.Author).Where(m => !m.IsDeleted).Skip(skip).Take(3).ToListAsync();


            return PartialView("_CoursePartial", courses);
        }

    }
}
