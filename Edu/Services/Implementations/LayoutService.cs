using Edu.DAL;
using Edu.Services.Interfaces;
using Edu.ViewModels;

namespace Edu.Services.Implementations
{
    public class LayoutService:ILayoutService
    {
      
            private readonly AppDbContext _context;
            private readonly IHttpContextAccessor _accessor;
            //private readonly IBasketService _basketService;

            public LayoutService(AppDbContext context,
                                IHttpContextAccessor accessor)
            {
                _context = context;
                _accessor = accessor;
                
            }

            public LayoutVM GetDatas()
            {
            
            var datas = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);

            return new LayoutVM {  Datas = datas };
        }
        
    }
}
