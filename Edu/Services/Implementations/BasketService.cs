using Edu.DAL;
using Edu.Models;
using Edu.Responses;
using Edu.Services.Interfaces;
using Edu.ViewModels;
using System.Text.Json;

namespace Edu.Services.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly ICourseService _courseService;

        public BasketService(AppDbContext context,
                             IHttpContextAccessor accessor,
                             ICourseService courseService)
        {
            _context = context;
            _accessor = accessor;
            _courseService = courseService;
        }

        public void AddCourse(List<BasketVM> basket, Course course)
        {
            BasketVM existCourse = basket.FirstOrDefault(m => m.Id == course.Id);

            if (existCourse != null)
            {
                existCourse.Count++;

            }
            else
            {
                basket.Add(new BasketVM
                {
                    Id = course.Id,
                    Count = 1
                });
            }

            _accessor.HttpContext.Session.SetString("basket", JsonSerializer.Serialize(basket));
        }

       
        public List<BasketVM> GetAll()
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
        public async Task<BasketDeleteResponse> Delete(int? id)
        {
            var basketSession = _accessor.HttpContext.Session.GetString("basket");
            var courses = JsonSerializer.Deserialize<List<BasketDetailVM>>(basketSession);

            var deleteCourse = courses.FirstOrDefault(m => m.Id == id);

            int deleteIndex = courses.IndexOf(deleteCourse);

            courses.RemoveAt(deleteIndex);

            _accessor.HttpContext.Session.SetString("basket", JsonSerializer.Serialize(courses));

            double total = 0;

            foreach (var item in courses)
            {
                Course courseDb = await _courseService.GetById(item.Id);
                total = total + (courseDb.Price * item.Count);
            }

            int count = courses.Sum(m => m.Count);
            BasketDeleteResponse response = new BasketDeleteResponse()
            {
                Count = count,
                Total = total
            };
            return response;
        }



    }
}
