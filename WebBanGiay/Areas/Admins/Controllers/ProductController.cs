using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Drawing;
using WebBanGiay.Models;
using WebBanGiay.Models.Dto;
using static NuGet.Packaging.PackagingConstants;

namespace WebBanGiay.Areas.Admins.Controllers
{
	[Area("Admins")]
	[Authorize(Roles = "Admin, Employee")]
	public class ProductController : Controller
	{
		private readonly DbwebGiayOnlineContext context;
		private readonly IWebHostEnvironment environment;

		public ProductController(DbwebGiayOnlineContext context, IWebHostEnvironment environment)
		{
			this.context = context;
			this.environment = environment;
		}

		public async Task<IActionResult> Index(int pg = 1)
		{
			const int pageSize = 10; // Số mục mỗi trang

			if (pg < 1) pg = 1; // Đảm bảo số trang >= 1

			// Đếm tổng số mục
			int recsCount = await context.Shoes.CountAsync();

			// Tạo đối tượng phân trang
			var pager = new Paginate(recsCount, pg, pageSize);

			// Xác định số mục cần bỏ qua
			int recSkip = (pg - 1) * pageSize;

			var product = await context.Shoes
				.OrderByDescending(s => s.ShoeId)
				.Include(s => s.Brand)
				.Include(s => s.Category)
				.Include(s => s.ShoeImages)

				.Skip(recSkip)
				.Take(pageSize)
				.ToListAsync();
			// Truyền dữ liệu phân trang vào View
			ViewBag.Pager = pager;

			return View(product);
		}
		[HttpGet]


		public IActionResult Create()
		{

			ViewBag.Brands = new SelectList(context.Brands.ToList(), "BrandId", "BrandName");
			ViewBag.Categories = new SelectList(context.ShoeCategories.ToList(), "CategoryId", "CategoryName");

			ViewBag.Images = context.ShoeImages.ToList();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Shoe model)
		{
			if (ModelState.IsValid)
			{
				// Tạo đối tượng Shoe mới
				var shoe = new Shoe
				{
					ShoeName = model.ShoeName,
					ShoeDescription = model.ShoeDescription,
					CareInstructions = model.CareInstructions,
					BrandId = model.BrandId,
					CategoryId = model.CategoryId,

					ShoeId = model.ShoeId,

					Price = model.Price,
					SalePrice = model.SalePrice,

					Sku = model.Sku,

				};

				// Thêm Shoe vào context
				context.Shoes.Add(shoe);
				context.SaveChanges();

				// Thêm các hình ảnh
				if (model.ImageUrls != null && model.ImageUrls.Any())
				{
					foreach (var imageUrl in model.ImageUrls)
					{
						if (!string.IsNullOrEmpty(imageUrl))
						{
							var shoeImage = new ShoeImage
							{
								ShoeId = shoe.ShoeId,
								ImageUrl = imageUrl
							};
							context.ShoeImages.Add(shoeImage);
						}
					}
				}

				context.SaveChanges();
				TempData["SuccessMessage"] = "Tạo sản phẩm thành công.";
				return RedirectToAction("Index");
			}

			// Nếu có lỗi, load lại các dropdown
			ViewBag.Brands = new SelectList(context.Brands.ToList(), "BrandId", "BrandName");
			ViewBag.Categories = new SelectList(context.ShoeCategories.ToList(), "CategoryId", "CategoryName");

			return View(model);
		}
		[HttpGet]
		public IActionResult GetCategoriesByBrand(int brandId)
		{
			var categories = context.ShoeCategories
				.Where(c => c.BrandId == brandId)
				.Select(c => new { c.CategoryId, c.CategoryName })
				.ToList();
			return Json(categories);
		}


		public async Task<IActionResult> Edit(int id)
		{
			var shoe = await context.Shoes
				.Include(s => s.ShoeImages)
				.FirstOrDefaultAsync(s => s.ShoeId == id);

			if (shoe == null)
			{
				return NotFound();
			}

			// Gán dữ liệu cho dropdown
			ViewBag.Brands = new SelectList(context.Brands.ToList(), "BrandId", "BrandName", shoe.BrandId);
			ViewBag.Categories = new SelectList(context.ShoeCategories.ToList(), "CategoryId", "CategoryName", shoe.CategoryId);

			// Gán dữ liệu hình ảnh
			ViewBag.Images = shoe.ShoeImages?.Select(img => img.ImageUrl).ToList();

			return View(shoe);
		}


		[HttpPost]
		public async Task<IActionResult> Edit(int id, Shoe model)
		{
			if (!ModelState.IsValid)
			{
				// Nếu có lỗi, load lại dropdown và hình ảnh
				ViewBag.Brands = new SelectList(context.Brands.ToList(), "BrandId", "BrandName", model.BrandId);
				ViewBag.Categories = new SelectList(context.ShoeCategories.ToList(), "CategoryId", "CategoryName", model.CategoryId);
				ViewBag.Images = model.ImageUrls;
				return View(model);
			}

			var shoe = await context.Shoes
				.Include(s => s.ShoeImages)
				.FirstOrDefaultAsync(s => s.ShoeId == id);

			if (shoe == null)
			{
				return NotFound();
			}

			// Cập nhật dữ liệu Shoe
			shoe.ShoeName = model.ShoeName;
			shoe.ShoeDescription = model.ShoeDescription;
			shoe.CareInstructions = model.CareInstructions;
			shoe.BrandId = model.BrandId;
			shoe.CategoryId = model.CategoryId;
			shoe.Price = model.Price;
			shoe.SalePrice = model.SalePrice;
			shoe.Sku = model.Sku;

			// Cập nhật hình ảnh
			if (model.ImageUrls != null)
			{
				// Xóa hình ảnh cũ
				context.ShoeImages.RemoveRange(shoe.ShoeImages);

				// Thêm hình ảnh mới
				foreach (var imageUrl in model.ImageUrls)
				{
					if (!string.IsNullOrEmpty(imageUrl))
					{
						shoe.ShoeImages.Add(new ShoeImage { ImageUrl = imageUrl });
					}
				}
			}

			await context.SaveChangesAsync();
			TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công.";
			return RedirectToAction("Index");
		}


		public IActionResult Delete(int id)
		{
			try
			{
				// Tìm sản phẩm kèm theo các bảng liên quan
				var shoe = context.Shoes

					.Include(s => s.ShoeImages)
					.FirstOrDefault(s => s.ShoeId == id);

				if (shoe == null)
				{
					TempData["ErrorMessage"] = "Không tìm thấy sản phẩm.";
					return RedirectToAction("Index");
				}

				// Xóa các ShoeImages và file ảnh tương ứng
				if (shoe.ShoeImages != null && shoe.ShoeImages.Any())
				{
					foreach (var image in shoe.ShoeImages)
					{
						if (!string.IsNullOrEmpty(image.ImageUrl))
						{
							var imagePath = Path.Combine(environment.WebRootPath, image.ImageUrl.TrimStart('/'));
							if (System.IO.File.Exists(imagePath))
							{
								System.IO.File.Delete(imagePath);
							}
						}
					}
					context.ShoeImages.RemoveRange(shoe.ShoeImages);
				}

				// Xóa sản phẩm chính
				context.Shoes.Remove(shoe);
				context.SaveChanges();

				TempData["SuccessMessage"] = "Xóa sản phẩm thành công.";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = $"Lỗi khi xóa sản phẩm: {ex.Message}";
				return RedirectToAction("Index");
			}
		}

		[Route("AddQuantity")]
		[HttpGet]
		public async Task<IActionResult> AddQuantity(int Id)
		{
			var productByQuantity = await context.ProductQuantities.Where(pq => pq.ShoeId == Id).ToListAsync();
			ViewBag.ProductByQuantity = productByQuantity;
			ViewBag.ShoeId = Id;
			return View();
		}
		[Route("StoreProductQuantity")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult StoreProductQuantity(ProductQuantityModel productQuantityModel)
		{
			var product = context.Shoes.Find(productQuantityModel.ShoeId);
			if (product == null)
			{
				return NotFound();
			}
			product.Quantity += productQuantityModel.Quantity;

			productQuantityModel.Quantity = productQuantityModel.Quantity;
			productQuantityModel.ShoeId = productQuantityModel.ShoeId;
			productQuantityModel.DateCreated = DateTime.Now;

			context.Add(productQuantityModel);
			context.SaveChangesAsync();
			TempData["SuccessMessage"] = "Thêm số lượng sản phẩm thành công.";
			return RedirectToAction("AddQuantity", "Product",new {Id = productQuantityModel.ShoeId});
		}


	}
}
