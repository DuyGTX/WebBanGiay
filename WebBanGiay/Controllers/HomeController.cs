using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return View();
        }
        public IActionResult Shop()
        {
            var product = context.Shoes
                .Include(s => s.ShoeItems).ThenInclude(si => si.Size)
                .Include(s => s.Brand)
                .Include(s => s.ShoeImages) // Thêm Include ShoeImages
                .OrderBy(s => s.ShoeId)
                .ToList();
            return View(product);
        }
        public IActionResult ShopDetail( int id)
        {
            var shoe = context.Shoes
                 .Include(s => s.Brand)
                 .Include(s => s.Category)
                 .Include(s => s.ShoeItems)
                     .ThenInclude(si => si.Colour)
                 .Include(s => s.ShoeItems)
                     .ThenInclude(si => si.Size)
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
                ShoeItems = shoe.ShoeItems.Select(si => new ShoeItemDetail
                {
                    ColourId = si.ColourId,
                    ColourName = si.Colour?.ColourName,  // Thêm ColourName
                    SizeId = si.SizeId,
                    SizeName = si.Size?.SizeName,
                    Price = si.Price,
                    SalePrice = si.SalePrice,
                    StockQuantity = si.StockQuantity,
                    Sku = si.Sku
                }).ToList(),
                ShoeImages = shoe.ShoeImages.Select(si => new ShoeImageDetail
                {
                    ImageUrl = si.ImageUrl,
                    ImageId = si.ImageId
                }).ToList()
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}