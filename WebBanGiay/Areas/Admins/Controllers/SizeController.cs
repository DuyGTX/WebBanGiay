using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using WebBanGiay.Models;
using WebBanGiay.Models.Dto;

namespace WebBanGiay.Areas.Admins.Controllers
{
	[Area("Admins")]
	public class SizeController : Controller
	{
		private readonly DbwebGiayOnlineContext context;
		private readonly IWebHostEnvironment environment;

		public SizeController(DbwebGiayOnlineContext context, IWebHostEnvironment environment)
		{
			this.context = context;
			this.environment = environment;
		}
		public IActionResult Index()
		{
			var sizes = context.Sizes
				.OrderBy(b => b.SizeId)
				.ToList();

			return View(sizes);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(SizeDto sizeDto)
		{


			// Kiểm tra nếu brand tồn tại
			if (sizeDto == null)
			{
				// Lưu thông báo vào TempData
				TempData["ErrorMessage"] = "Size không tồn tại";

				// Trả lại view với dữ liệu hiện tại
				return View(sizeDto);
			}


			Size size = new Size()
			{
				SizeName = sizeDto.SizeName,
				SortOrder = sizeDto.SortOrder,
			};
			// Kiểm tra nếu đã tồn tại
			var existingCategory = context.Sizes
				.FirstOrDefault(s => s.SizeName == sizeDto.SizeName);

			if (existingCategory != null)
			{
				// Thêm thông báo lỗi nếu tên đã tồn tại
				ModelState.AddModelError("SizeName", "Size đã tồn tại.");
				return View(sizeDto); // Trả lại view với thông báo lỗi
			}

			// Lưu ShoeCategory vào cơ sở dữ liệu
			context.Sizes.Add(size);
			context.SaveChanges();

			// Thêm thông báo thành công nếu cần
			TempData["SuccessMessage"] = "Thêm size thành công!";

			return RedirectToAction("Index", "Size");
		}
		public IActionResult Edit(int id)
		{
			var size = context.Sizes.Find(id);
			if (size == null)
			{
				return RedirectToAction("Index", "Size");
			}
			var sizeDto = new SizeDto()
			{
				SizeId = size.SizeId,  
				SizeName = size.SizeName,

			};

			return View(sizeDto);
		}


		[HttpPost]
		public IActionResult Edit(SizeDto sizeDto)
		{
			// Kiểm tra ModelState trước
			if (!ModelState.IsValid)
			{
				TempData["ErrorMessage"] = "Vui lòng kiểm tra lại thông tin nhập";
				return View(sizeDto);
			}

			// Tìm kiếm để đảm bảo tồn tại
			var size = context.Sizes.Find(sizeDto.SizeId);
			if (size == null)
			{
				TempData["ErrorMessage"] = "Không tìm thấy size";
				return RedirectToAction("Index");
			}

			try
			{
				// Kiểm tra xem tên thương hiệu đã tồn tại chưa (loại trừ brand hiện tại)
				var existingSize = context.Sizes
					.FirstOrDefault(s => s.SizeName.ToLower() == sizeDto.SizeName.ToLower()
									&& s.SizeId != sizeDto.SizeId);

				if (existingSize != null)
				{
					TempData["ErrorMessage"] = "Size này đã tồn tại!";
					return View(sizeDto);
				}

				// Cập nhật thông tin
				size.SizeName = sizeDto.SizeName;
				size.SortOrder = sizeDto.SortOrder;

				context.Sizes.Update(size);
				context.SaveChanges();
				TempData["SuccessMessage"] = "Cập nhật thành công";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
				return View(sizeDto);
			}
		}


		public IActionResult Delete(int id)
		{
			var size = context.Sizes.Find(id);
			if (size == null)
			{
				return RedirectToAction("Index", "Size");

			}

			context.Sizes.Remove(size);
			context.SaveChanges(true);
			return RedirectToAction("Index", "Size");
		}
	}
}
