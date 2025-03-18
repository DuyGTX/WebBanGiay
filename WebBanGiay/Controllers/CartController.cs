using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Models;
using WebBanGiay.Models.ViewModels;
using WebBanGiay.Repository;

namespace WebBanGiay.Controllers
{
    public class CartController : Controller
    {
        private readonly DbwebGiayOnlineContext _dataContext;
        public CartController(DbwebGiayOnlineContext _context) 
        {
            _dataContext = _context;
        }
        public IActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartVM = new()
            {

                CartItems = cartItems,

                GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
            };

        
            return View(cartVM);
        }
        public ActionResult Checkout ()
        {
            return View("~/Views/Home/Checkout.cshtml");
        }
        public async Task<IActionResult> Add(int id)
        {
            var shoes = await _dataContext.Shoes
                .Include(s => s.ShoeImages)
                .FirstOrDefaultAsync(s => s.ShoeId == id) ?? throw new ArgumentException("Không tìm thấy sản phẩm");

            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel cartItems = cart.Where(c => c.ShoeId == id).FirstOrDefault();

            if (cartItems == null)
            {
                cart.Add(new CartItemModel(shoes));
            }
            else
            {
                cartItems.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);
            TempData["SuccessMessage"] = "Thêm sản phẩm vào giỏ hàng thành công.";

			return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

            CartItemModel cartItem = cart.Where(c=>c.ShoeId== id).FirstOrDefault();
            if(cartItem.Quantity >1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p=>p.ShoeId == id);
            }
            if(cart.Count == 0) {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Increase(int id)
        {

            Shoe shoe = await _dataContext.Shoes.Where(p=>p.ShoeId==id).FirstOrDefaultAsync()
                ;
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

            CartItemModel cartItem = cart.FirstOrDefault(c => c.ShoeId == id);
            if (cartItem.Quantity >= 1 && shoe.Quantity>cartItem.Quantity) // Max quantity is 10
            {
                ++cartItem.Quantity;
            }
            else
            {
                cartItem.Quantity = shoe.Quantity;
                TempData["SuccessMessage"] = "Đã đạt đến số lượng tối đa của sản phẩm.";
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

            cart.RemoveAll(p=>p.ShoeId == id);
           

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

			TempData["SuccessMessage"] = "Xóa sản phẩm khỏi giỏ hàng thành công.";
			return RedirectToAction("Index");
        }
    }
}
