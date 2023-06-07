using Edu.DAL;
using Edu.Models;

namespace Edu.Services.Interfaces
{
    public interface ICourseService
    {

        public  Task<IEnumerable<Course>> GetAll();
        public Task<Course> GetById(int? id);

    }
}
