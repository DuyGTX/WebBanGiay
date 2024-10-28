using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Models;
using WebBanGiay.Models.Dto;

namespace WebBanGiay.Areas.Admins.Controllers
{
	[Area("Admins")]
	public class ProductController : Controller
	{
		private readonly DbwebGiayOnlineContext context;
		private readonly IWebHostEnvironment environment;

		public ProductController(DbwebGiayOnlineContext context, IWebHostEnvironment environment)
		{
			this.context = context;
			this.environment = environment;
		}

		public IActionResult Index()
		{
			var product = context.Shoes
				.Include(s => s.ShoeItems).ThenInclude(si => si.Size)
				.Include(s => s.Brand)
				.Include(s => s.ShoeImages) // Thêm Include ShoeImages
				.OrderBy(s => s.ShoeId)
				.ToList();
			return View(product);
		}
		public IActionResult ViewDetail()
		{
			var product = context.Shoes
				.Include(s => s.ShoeItems).ThenInclude(si => si.Size)
				.Include(s => s.Brand)
				.Include(s => s.Category)
				.Include(s => s.ShoeImages) // Thêm Include ShoeImages
				.OrderBy(s => s.ShoeId)
				.ToList();
			return View(product);
		}
	}



}
