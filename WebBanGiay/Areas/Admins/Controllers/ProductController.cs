using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
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
				
				.Include(s => s.Brand)
				.Include(s => s.ShoeImages)
				
				.Include(s => s.ShoeImages)
				.OrderBy(s => s.ShoeId)
				.ToList();
			return View(product);
		}
		public IActionResult ViewDetail(int id)
		{
			var shoe = context.Shoes
				.Include(s => s.Brand)
				.Include(s => s.Category)
				
				.Include(s => s.ShoeSizes)
					.ThenInclude(sis => sis.Size)
				.Include(sc => sc.ShoeColours)
					.ThenInclude(sic => sic.Colour)


				.Include(s => s.ShoeImages)
				.FirstOrDefault(s => s.ShoeId == id);

			if (shoe == null)
			{
				return NotFound();
			}

			var productDto = new ProductDto
			{
				ShoeId = shoe.ShoeId,
				ShoeName = shoe.ShoeName,
				ShoeDescription = shoe.ShoeDescription,
				CareInstructions = shoe.CareInstructions,
				BrandId = shoe.BrandId,
				CategoryId = shoe.CategoryId,
                Price = shoe.Price,
                SalePrice = shoe.SalePrice,
                Sku = shoe.Sku,
                Colours = shoe.ShoeColours.Select(sic => new ColourDetail
                {
                    ColourId = sic.ColourId,
                    ColourName = sic.Colour?.ColourName,
                    StockQuantity = sic.StockQuantity
                }).ToList(),
                Sizes = shoe.ShoeSizes.Select(sis => new SizeDetail
                {
                    SizeId = sis.SizeId,
                    SizeName = sis.Size?.SizeName,
                    StockQuantity = sis.StockQuantity
                }).ToList(),
                ShoeImages = shoe.ShoeImages.Select(img => new ShoeImageDetail
				{
					ImageId = img.ImageId,
					ImageUrl = img.ImageUrl
				}).ToList(),
				ImageUrls = shoe.ShoeImages.Select(img => img.ImageUrl).ToList()
			};

			// Thêm thông tin bổ sung vào ViewBag
			ViewBag.BrandName = shoe.Brand?.BrandName;
			ViewBag.CategoryName = shoe.Category?.CategoryName;
			

			return View(productDto);
		}


		public IActionResult Create()
		{

			ViewBag.Brands = context.Brands.ToList();
			ViewBag.Categories = context.ShoeCategories.ToList();

			ViewBag.Images =context.ShoeImages.ToList();
			return View();
		}

		[HttpPost]
		public IActionResult Create(ProductDto model)
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
				return RedirectToAction("Index");
			}

			// Nếu có lỗi, load lại các dropdown
			ViewBag.Brands = context.Brands.ToList();
			ViewBag.Categories = context.ShoeCategories.ToList();
			
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
		[HttpGet]
		public IActionResult Edit(int id)
		{
			// Retrieve the product details based on the id
			var shoe = context.Shoes
				.Include(s => s.Brand)
				.Include(s => s.Category)
				.Include(s => s.ShoeImages)
				.FirstOrDefault(s => s.ShoeId == id);

			if (shoe == null)
			{
				return NotFound();
			}

			// Map the retrieved product data to the ProductDto for editing
			var productDto = new ProductDto
			{
				ShoeId = shoe.ShoeId,
				ShoeName = shoe.ShoeName,
				ShoeDescription = shoe.ShoeDescription,
				CareInstructions = shoe.CareInstructions,
				BrandId = shoe.BrandId,
				CategoryId = shoe.CategoryId,				
				Price = shoe.Price,
				SalePrice = shoe.SalePrice,
				Sku = shoe.Sku,
				
				ShoeImages = shoe.ShoeImages.Select(si => new ShoeImageDetail
				{
					ImageUrl = si.ImageUrl,
					ImageId = si.ImageId
				}).ToList()
			};

			// Load dropdown lists for Brands and Categories
			ViewBag.Brands = context.Brands.ToList();
			ViewBag.Categories = context.ShoeCategories.ToList();

			return View(productDto);
		}

		[HttpPost]
		public IActionResult Edit(ProductDto model)
		{
			if (ModelState.IsValid)
			{
				// Retrieve the existing Shoe entry
				var shoe = context.Shoes
					//.Include(sc=>sc.ShoeSizes)
					.Include(s => s.ShoeImages)
					.FirstOrDefault(s => s.ShoeId == model.ShoeId);

				if (shoe == null)
				{
					return NotFound();
				}

				// Update the main product fields
				shoe.ShoeName = model.ShoeName;
				shoe.ShoeDescription = model.ShoeDescription;
				shoe.CareInstructions = model.CareInstructions;
				shoe.BrandId = model.BrandId;
				shoe.CategoryId = model.CategoryId;
				shoe.Price = model.Price;
				shoe.SalePrice = model.SalePrice;
				shoe.Sku = model.Sku;

				// Update ShoeImages (clear existing and add new ones)
				context.ShoeImages.RemoveRange(shoe.ShoeImages);
				if (model.ShoeImages != null && model.ShoeImages.Any())
				{
					foreach (var image in model.ShoeImages)
					{
						if (!string.IsNullOrEmpty(image.ImageUrl))
						{
							var shoeImage = new ShoeImage
							{
								ShoeId = shoe.ShoeId,
								ImageUrl = image.ImageUrl
							};
							context.ShoeImages.Add(shoeImage);
						}
					}
				}

				context.SaveChanges();
				return RedirectToAction("Index");
			}

			// Reload dropdown lists in case of validation error
			ViewBag.Brands = context.Brands.ToList();
			ViewBag.Categories = context.ShoeCategories.ToList();
			return View(model);
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




	}




}
