﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Models;
using WebBanGiay.Models.Dto;

namespace WebBanGiay.Areas.Admins.Controllers
{
    [Area("Admins")]
	[Authorize(Roles = "Admin, Employee")]
	public class CategoryController : Controller
    {
        private readonly DbwebGiayOnlineContext context;
		private readonly IWebHostEnvironment environment;

		public CategoryController(DbwebGiayOnlineContext context, IWebHostEnvironment environment) 
        {
            this.context =context;
			this.environment = environment;
		}
		public IActionResult Index(int pg = 1)
		{
			// Lấy danh sách ShoeCategories và sắp xếp
			var categoris = context.ShoeCategories
				.Include(c => c.Brand) // Bao gồm Brand
				.OrderBy(c => c.CategoryId)
				.ToList();

			const int pageSize = 10; // 10 items/trang

			// Nếu pg < 1 thì đặt pg = 1
			if (pg < 1)
			{
				pg = 1;
			}

			int recsCount = categoris.Count(); // Tổng số danh mục
			var pager = new Paginate(recsCount, pg, pageSize);

			// Số lượng cần bỏ qua
			int recSkip = (pg - 1) * pageSize;

			// Lấy dữ liệu đã phân trang
			var data = categoris.Skip(recSkip).Take(pageSize).ToList();

			// Truyền dữ liệu phân trang vào View
			ViewBag.Pager = pager;

			return View(data); // Truyền `data` thay vì toàn bộ `categoris`
		}

		public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
		public IActionResult Create(CategoryDto categoryDto)
		{
			// Tìm kiếm Brand dựa trên BrandId từ categoryDto
			var brand = context.Brands.FirstOrDefault(b => b.BrandId == categoryDto.BrandId);

			// Kiểm tra nếu brand tồn tại
			if (brand == null)
			{
				// Lưu thông báo vào TempData
				TempData["ErrorMessage"] = "Brand không tồn tại";

				// Trả lại view với dữ liệu hiện tại
				return View(categoryDto);
			}

			// Tạo đối tượng ShoeCategory và gán BrandId
			ShoeCategory category = new ShoeCategory()
			{
				CategoryName = categoryDto.CategoryName,
				BrandId = brand.BrandId // Gán BrandId từ Brand tìm được
			};

			// Kiểm tra nếu CategoryName đã tồn tại
			var existingCategory = context.ShoeCategories
				.FirstOrDefault(c => c.CategoryName == categoryDto.CategoryName);

			if (existingCategory != null)
			{
				// Thêm thông báo lỗi nếu tên danh mục đã tồn tại
				ModelState.AddModelError("CategoryName", "Tên danh mục đã tồn tại.");
				return View(categoryDto); // Trả lại view với thông báo lỗi
			}

			// Lưu ShoeCategory vào cơ sở dữ liệu
			context.ShoeCategories.Add(category);
			context.SaveChanges();

			// Thêm thông báo thành công nếu cần
			TempData["SuccessMessage"] = "Thêm danh mục thành công!";

			return RedirectToAction("Index", "Category");
		}

		public IActionResult Edit(int id)
		{
			var category = context.ShoeCategories.Find(id);
			if (category == null)
			{
				return RedirectToAction("Index", "Category");
			}
			var categoryDto = new CategoryDto()
			{
				CategoryId = category.CategoryId,  // Thêm CategoryId
				CategoryName = category.CategoryName,
				BrandId = category.BrandId
			};

			return View(categoryDto);
		}
		
		[HttpPost]
		public IActionResult Edit(CategoryDto categoryDto)
		{
			// Kiểm tra ModelState trước
			if (!ModelState.IsValid)
			{
				TempData["ErrorMessage"] = "Vui lòng kiểm tra lại thông tin nhập";
				return View(categoryDto);
			}

			// Tìm kiếm category để đảm bảo tồn tại
			var category = context.ShoeCategories.Find(categoryDto.CategoryId);
			if (category == null)
			{
				TempData["ErrorMessage"] = "Không tìm thấy danh mục";
				return RedirectToAction("Index");
			}

			// Tìm kiếm Brand
			var brand = context.Brands.Find(categoryDto.BrandId);
			if (brand == null)
			{
				TempData["ErrorMessage"] = $"Brand với ID {categoryDto.BrandId} không tồn tại";
				return View(categoryDto);
			}

			try
			{
				// Cập nhật thông tin
				category.CategoryName = categoryDto.CategoryName;
				category.BrandId = categoryDto.BrandId;

				context.ShoeCategories.Update(category);
				context.SaveChanges();

				TempData["SuccessMessage"] = "Cập nhật thành công";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
				return View(categoryDto);
			}
		}

		
		public IActionResult Delete(int id)
		{
			var category = context.ShoeCategories.Find(id);
			if (category == null)
			{
				return RedirectToAction("Index", "Category");

			}
			
			context.ShoeCategories.Remove(category);
			context.SaveChanges(true);
			return RedirectToAction("Index", "Category");
		}

	}
}
