using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using WebBanGiay.Models;
using WebBanGiay.Models.Dto;

namespace WebBanGiay.Areas.Admins.Controllers
{
	[Area("Admins")]
	public class BrandController : Controller
	{
		private readonly DbwebGiayOnlineContext context;
		private readonly IWebHostEnvironment environment;

		public BrandController(DbwebGiayOnlineContext context, IWebHostEnvironment environment)
		{
			this.context = context;
			this.environment = environment;
		}
		public IActionResult Index()
		{
			var brands = context.Brands 
				.OrderBy(b => b.BrandId)
				.ToList();

			return View(brands);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(BrandDto brandDto)
		{
			

			// Kiểm tra nếu brand tồn tại
			if (brandDto == null)
			{
				// Lưu thông báo vào TempData
				TempData["ErrorMessage"] = "Brand không tồn tại";

				// Trả lại view với dữ liệu hiện tại
				return View(brandDto);
			}


			Brand brand = new Brand()
			{
				BrandName = brandDto.BrandName,
			};
			// Kiểm tra nếu đã tồn tại
			var existingCategory = context.Brands
				.FirstOrDefault(b => b.BrandName == brandDto.BrandName);

			if (existingCategory != null)
			{
				// Thêm thông báo lỗi nếu tên đã tồn tại
				ModelState.AddModelError("BrandName", "Tên thương hiệu đã tồn tại.");
				return View(brandDto); // Trả lại view với thông báo lỗi
			}

			// Lưu ShoeCategory vào cơ sở dữ liệu
			context.Brands.Add(brand);
			context.SaveChanges();

			// Thêm thông báo thành công nếu cần
			TempData["SuccessMessage"] = "Thêm thương hiệu thành công!";

			return RedirectToAction("Index", "Brand");
		}
		public IActionResult Edit(int id)
		{
			var brand = context.Brands.Find(id);
			if (brand == null)
			{
				return RedirectToAction("Index", "Category");
			}
			var brandDto = new BrandDto()
			{
				BrandId = brand.BrandId,  // Thêm CategoryId
				BrandName = brand.BrandName,
				
			};

			return View(brandDto);
		}

		
		[HttpPost]
		public IActionResult Edit(BrandDto brandDto)
		{
			// Kiểm tra ModelState trước
			if (!ModelState.IsValid)
			{
				TempData["ErrorMessage"] = "Vui lòng kiểm tra lại thông tin nhập";
				return View(brandDto);
			}

			// Tìm kiếm để đảm bảo tồn tại
			var brand = context.Brands.Find(brandDto.BrandId);
			if (brand == null)
			{
				TempData["ErrorMessage"] = "Không tìm thấy thương hiệu";
				return RedirectToAction("Index");
			}

			try
			{
				// Kiểm tra xem tên thương hiệu đã tồn tại chưa (loại trừ brand hiện tại)
				var existingBrand = context.Brands
					.FirstOrDefault(b => b.BrandName.ToLower() == brandDto.BrandName.ToLower()
									&& b.BrandId != brandDto.BrandId);

				if (existingBrand != null)
				{
					TempData["ErrorMessage"] = "Tên thương hiệu này đã tồn tại!";
					return View(brandDto);
				}

				// Cập nhật thông tin
				brand.BrandName = brandDto.BrandName;

				context.Brands.Update(brand);
				context.SaveChanges();
				TempData["SuccessMessage"] = "Cập nhật thành công";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
				return View(brandDto);
			}
		}


		public IActionResult Delete(int id)
		{
			var brand = context.Brands.Find(id);
			if (brand == null)
			{
				return RedirectToAction("Index", "Brand");

			}

			context.Brands.Remove(brand);
			context.SaveChanges(true);
			return RedirectToAction("Index", "Brand");
		}
	}
}
