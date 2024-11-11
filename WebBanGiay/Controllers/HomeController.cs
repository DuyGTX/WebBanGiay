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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DbwebGiayOnlineContext context)
        {
            _logger = logger;
            this.context = context;
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
        public IActionResult ShopDetail( int id)
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

        public IActionResult ShoppingCart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
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