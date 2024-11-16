using Microsoft.AspNetCore.Mvc;
using WebBanGiay.Models.Dto;
using WebBanGiay.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using Microsoft.AspNetCore.Authorization;

namespace WebBanGiay.Areas.Admins.Controllers
{
	[Area("Admins")]
    [Authorize]
    public class ColourController : Controller
	{
		private readonly DbwebGiayOnlineContext context;
		private readonly IWebHostEnvironment environment;

		public ColourController(DbwebGiayOnlineContext context, IWebHostEnvironment environment)
		{
			this.context = context;
			this.environment = environment;
		}
		public IActionResult Index()
		{
			var colours = context.Colours
				.OrderBy(b => b.ColourId)
				.ToList();

			return View(colours);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(ColourDto colourDto)
		{


			// Kiểm tra nếu brand tồn tại
			if (colourDto == null)
			{
				// Lưu thông báo vào TempData
				TempData["ErrorMessage"] = "Colour không tồn tại";

				// Trả lại view với dữ liệu hiện tại
				return View(colourDto);
			}


			Colour colour = new Colour()
			{
				ColourName = colourDto.ColourName,
			};
			// Kiểm tra nếu đã tồn tại
			var existingCategory = context.Colours
				.FirstOrDefault(c => c.ColourName == colourDto.ColourName);

			if (existingCategory != null)
			{
				// Thêm thông báo lỗi nếu tên đã tồn tại
				ModelState.AddModelError("ColourName", "Tên màu đã tồn tại.");
				return View(colourDto); // Trả lại view với thông báo lỗi
			}

			// Lưu ShoeCategory vào cơ sở dữ liệu
			context.Colours.Add(colour);
			context.SaveChanges();

			// Thêm thông báo thành công nếu cần
			TempData["SuccessMessage"] = "Thêm màu thành công!";

			return RedirectToAction("Index", "Colour");
		}
		public IActionResult Edit(int id)
		{
			var colour = context.Colours.Find(id);
			if (colour == null)
			{
				return RedirectToAction("Index", "Colour");
			}
			var colourDto = new ColourDto()
			{
				ColourId = colour.ColourId,  // Thêm CategoryId
				ColourName = colour.ColourName,

			};

			return View(colourDto);
		}


		[HttpPost]
		public IActionResult Edit(ColourDto colourDto)
		{
			// Kiểm tra ModelState trước
			if (!ModelState.IsValid)
			{
				TempData["ErrorMessage"] = "Vui lòng kiểm tra lại thông tin nhập";
				return View(colourDto);
			}

			// Tìm kiếm để đảm bảo tồn tại
			var colour = context.Colours.Find(colourDto.ColourId);
			if (colour == null)
			{
				TempData["ErrorMessage"] = "Không tìm thấy tên màu";
				return RedirectToAction("Index");
			}

			try
			{
				// Kiểm tra xem tên thương hiệu đã tồn tại chưa (loại trừ brand hiện tại)
				var existingColour = context.Colours
					.FirstOrDefault(c => c.ColourName.ToLower() == colourDto.ColourName.ToLower()
									&& c.ColourId != c.ColourId);

				if (existingColour != null)
				{
					TempData["ErrorMessage"] = "Tên màu này đã tồn tại!";
					return View(colourDto);
				}

				// Cập nhật thông tin
				colour.ColourName = colourDto.ColourName;

				context.Colours.Update(colour);
				context.SaveChanges();
				TempData["SuccessMessage"] = "Cập nhật thành công";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
				return View(colourDto);
			}
		}


		public IActionResult Delete(int id)
		{
			var colour = context.Colours.Find(id);
			if (colour == null)
			{
				return RedirectToAction("Index", "Colour");

			}

			context.Colours.Remove(colour);
			context.SaveChanges(true);
			return RedirectToAction("Index", "Colour");
		}
	}



}

