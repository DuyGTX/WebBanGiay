using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using WebBanGiay.Models;
using WebBanGiay.Models.Dto;

namespace WebBanGiay.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbwebGiayOnlineContext context;
        private readonly UserManager<AppUserModel> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DbwebGiayOnlineContext context, UserManager<AppUserModel> userManager)
        {
            _logger = logger;
            this.context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home()
        {
            var product = context.Shoes

                .Include(s => s.Brand)
                .Include(s => s.ShoeImages)

                .Include(s => s.ShoeImages)
                .OrderBy(s => s.ShoeId)
                .ToList();
            return View(product);   
        }
        public IActionResult Shop()
        {
            var product = context.Shoes

                .Include(s => s.Brand)
                .Include(s => s.ShoeImages)

                .Include(s => s.ShoeImages)
                .OrderBy(s => s.ShoeId)
                .ToList();
            return View(product);
        }
        public IActionResult ShopDetail(int id)
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

            // Lấy sản phẩm liên quan (cùng category hoặc brand, không bao gồm sản phẩm hiện tại)
            var relatedProducts = context.Shoes
                .Include(s => s.ShoeImages)
                .Include(s => s.ShoeColours)
                    .ThenInclude(sc => sc.Colour)
                .Include(s => s.ShoeSizes)
                    .ThenInclude(ss => ss.Size)
                .Where(s => (s.CategoryId == shoe.CategoryId || s.BrandId == shoe.BrandId)
                            && s.ShoeId != shoe.ShoeId)
                .Take(4)  // Chỉ lấy 4 sản phẩm
                .Select(s => new ProductDto
                {
                    ShoeId = s.ShoeId,
                    ShoeName = s.ShoeName,
                    Price = s.Price,
                    Sold = s.Sold,
                    Quantity = s.Quantity,

                    SalePrice = s.SalePrice,
                    ShoeImages = s.ShoeImages.Select(img => new ShoeImageDetail
                    {
                        ImageId = img.ImageId,
                        ImageUrl = img.ImageUrl
                    }).ToList(),
                    ImageUrls = s.ShoeImages.Select(img => img.ImageUrl).ToList(),
                    Colours = s.ShoeColours.Select(sc => new ColourDetail
                    {
                        ColourId = sc.ColourId,
                        ColourName = sc.Colour.ColourName,
                        StockQuantity = sc.StockQuantity
                    }).ToList(),
                    Sizes = s.ShoeSizes.Select(ss => new SizeDetail
                    {
                        SizeId = ss.SizeId,
                        SizeName = ss.Size.SizeName,
                        StockQuantity = ss.StockQuantity
                    }).ToList()
                })
                .ToList();

            var productDto = new ProductDto
            {
                ShoeId = shoe.ShoeId,
                ShoeName = shoe.ShoeName,
                ShoeDescription = shoe.ShoeDescription,
                CareInstructions = shoe.CareInstructions,
                BrandId = shoe.BrandId,
                Quantity = shoe.Quantity,
                Sold = shoe.Sold,
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
            ViewBag.RelatedProducts = relatedProducts;

            return View(productDto);
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            var products = await context.Shoes
                .Where(p => p.ShoeName.Contains(searchTerm))
                .Include(s => s.ShoeImages)
                .ToListAsync();

            ViewBag.Keyword = searchTerm;

            // Nếu không tìm thấy sản phẩm nào, thêm thông báo
            if (!products.Any())
            {
                ViewBag.Message = "Không tìm thấy bất kỳ kết quả nào với từ khóa trên.";
            }

            return View(products);
        }

        public async Task<IActionResult> Wishlist()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var wishlistProducts = await context.WishLists
                .Where(w => w.UserId == user.Id)
                .Include(w => w.Shoe)
                    .ThenInclude(s => s.ShoeImages)
                .Select(w => new WishList
                {
                    Id = w.Id,
                    ShoeId = w.ShoeId,
                    UserId = w.UserId,
                    Shoe = w.Shoe,
                    ImageUrls = w.Shoe.ShoeImages.Select(si => si.ImageUrl).ToList()
                })
                .ToListAsync();

            return View(wishlistProducts);
        }

        [HttpPost]
        public async Task<IActionResult> AddWishlist(int Id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Json(new { success = false, message = "Vui lòng đăng nhập để thêm sản phẩm yêu thích." });
                }

                // Kiểm tra xem sản phẩm có tồn tại không
                var shoe = await context.Shoes.FindAsync(Id);
                if (shoe == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại." });
                }

                // Kiểm tra nếu sản phẩm đã có trong danh sách yêu thích
                var existingWishlistItem = await context.WishLists
                    .FirstOrDefaultAsync(w => w.ShoeId == Id && w.UserId == user.Id);

                if (existingWishlistItem != null)
                {
                    return Json(new { success = false, message = "Sản phẩm đã tồn tại trong danh sách yêu thích." });
                }

                var wishList = new WishList
                {
                    ShoeId = Id,
                    UserId = user.Id
                };

                context.WishLists.Add(wishList);
                await context.SaveChangesAsync();

                return Json(new { success = true, message = "Đã thêm sản phẩm vào mục yêu thích." });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ở đây nếu có
                return Json(new { success = false, message = "Đã xuất hiện lỗi khi thêm sản phẩm vào mục yêu thích" });
            }
        }
        public async Task<IActionResult> DeleteWishlist(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var wishlistItem = await context.WishLists
                    .FirstOrDefaultAsync(w => w.Id == id && w.UserId == user.Id);

                if (wishlistItem == null)
                {
                    return NotFound();
                }

                context.WishLists.Remove(wishlistItem);
                await context.SaveChangesAsync();

                return RedirectToAction("Wishlist");
            }
            catch
            {
                return RedirectToAction("Wishlist");
            }
        }

        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        //public ActionResult GetImageName(string ShoeName) //dùng để lấy ra tên hình ảnh trong session  lưu bảng tạm trong giỏ hàng 
        //{
        //    var product = context.Shoes.FirstOrDefault(p => p.ShoeName == ShoeName);
        //    return Content(JsonConvert.SerializeObject(product.ProImage), "application/json");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}