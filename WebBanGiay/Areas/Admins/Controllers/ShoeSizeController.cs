using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using WebBanGiay.Models;
using WebBanGiay.Models.Dto;

namespace WebBanGiay.Areas.Admins.Controllers
{
	[Area("Admins")]
	public class ShoeSizeController : Controller
	{
		private readonly DbwebGiayOnlineContext context;
		private readonly IWebHostEnvironment environment;

		public ShoeSizeController(DbwebGiayOnlineContext context, IWebHostEnvironment environment)
		{
			this.context = context;
			this.environment = environment;
		}
		public IActionResult Index()
		{
			// Lấy danh sách ShoeItemSize bao gồm thông tin về Size
			var shoeSizes = context.ShoeSizes
				.Include(s => s.Size) // Bao gồm Size để lấy dữ liệu từ bảng Size
				.OrderBy(b => b.ShoeId) // Sắp xếp theo ShoeItemId
				.ToList();

			// Trả về view với danh sách ShoeItemSizes
			return View(shoeSizes);
		}

		public IActionResult Create()
		{
			// Lấy danh sách kích thước từ cơ sở dữ liệu
			var sizes = context.Sizes.ToList();

			// Chuyển đổi danh sách thành SelectList để sử dụng trong dropdown
			ViewBag.SizeList = new SelectList(sizes, "SizeId", "SizeName");

			return View();
		}

		[HttpPost]
		public IActionResult Create(ShoeSizeDto shoeSize)
		{
			
			// Kiểm tra nếu shoeItemSize tồn tại
			if (shoeSize == null)
			{
				TempData["ErrorMessage"] = "Dữ liệu không hợp lệ.";
				return View(shoeSize);
			}

            // Tạo đối tượng ShoeItemSize
            ShoeSize sizeItem = new ShoeSize()
			{
					ShoeId = shoeSize.ShoeId,
					SizeId = shoeSize.SizeId,
					StockQuantity = shoeSize.StockQuantity
			};

			try
			{
				// Thêm đối tượng ShoeItemSize vào cơ sở dữ liệu
				context.ShoeSizes.Add(sizeItem);
				context.SaveChanges();

				// Thêm thông báo thành công
				TempData["SuccessMessage"] = "Thêm size thành công!";
				return RedirectToAction("Index", "ShoeItemSize");
			}
			catch (DbUpdateException ex)
			{
				// In ra thông báo lỗi
				TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.InnerException?.Message ?? ex.Message}";
				return View(sizeItem);
			}
		}

		public IActionResult Edit(int id)
		{
			var shoeitemsize = context.ShoeSizes.Find(id);
			if (shoeitemsize == null)
			{
				return RedirectToAction("Index", "ShoeItemSize");
			}
			var shoeitemsizeDto = new ShoeSizeDto()
			{
				SizeId = shoeitemsize.SizeId,
				StockQuantity = shoeitemsize.StockQuantity

			};

			return View(shoeitemsizeDto);
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
			var shoeitemsize = context.ShoeSizes.Find(id);
			if (shoeitemsize == null)
			{
				return RedirectToAction("Index", "ShoeItemSize");

			}

			context.ShoeSizes.Remove(shoeitemsize);
			context.SaveChanges(true);
			return RedirectToAction("Index", "ShoeItemSize");
		}
	}
}
