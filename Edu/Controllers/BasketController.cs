using Edu.DAL;
using Edu.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Edu.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public BasketController(AppDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BasketDetailVM> basketList = new();
            var basketSession = _accessor.HttpContext.Session.GetString("basket");

            if (basketSession != null)
            {
                List<BasketVM> basketDatas = JsonSerializer.Deserialize<List<BasketVM>>(basketSession);

                foreach (var item in basketDatas)
                {
                    var course = await _context.Courses.Include(c=>c.CourseImgs).FirstOrDefaultAsync(m => m.Id == item.Id);

                    if (course != null)
                    {
                        BasketDetailVM basketDetail = new()
                        {
                            Id = course.Id,
                            Name = course.Name,
                            Image = course.CourseImgs.Where(m => !m.IsDeleted).Where(c=>c.IsItMain).FirstOrDefault().Url,
                            Count = item.Count,
                            Price = course.Price,
                            TotalPrice = item.Count * course.Price
                        };

                        basketList.Add(basketDetail);
                    }
                }
            }

            return View(basketList);
        }

        [HttpPost]
    
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var basketSession = _accessor.HttpContext.Session.GetString("basket");

            var courses = JsonSerializer.Deserialize<List<BasketDetailVM>>(basketSession);

            var deletecourse = courses.FirstOrDefault(m => m.Id == id);

            int deleteIndex = courses.IndexOf(deletecourse);

            courses.RemoveAt(deleteIndex);

            _accessor.HttpContext.Session.SetString("basket", JsonSerializer.Serialize(courses));

            return RedirectToAction(nameof(Index));
        }
    }
}
