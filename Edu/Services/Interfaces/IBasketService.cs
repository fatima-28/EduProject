using Edu.Models;
using Edu.Responses;
using Edu.ViewModels;

namespace Edu.Services.Interfaces
{
    public interface IBasketService
    {
        List<BasketVM> GetAll();
        Task<BasketDeleteResponse> Delete(int? id);
        public void AddCourse(List<BasketVM> basket, Course Course);
       
    }
}
