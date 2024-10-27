using Microsoft.AspNetCore.Mvc;
using WebBanGiay.Models;

namespace WebBanGiay.Areas.Admins.Controllers
{
    [Area("Admins")] // Đảm bảo dòng này có trong controller
    public class CategoryController : Controller
    {
        private readonly DbwebGiayOnlineContext context;
        public CategoryController(DbwebGiayOnlineContext context) 
        {
            this.context =context;
        }
        public IActionResult Index()
        {
            var categoris = context.ShoeCategories.OrderByDescending(c=>c.CategoryId).ToList();
            return View(categoris);
        }
		public IActionResult Create()
		{
			return View();
		}

	}
}
