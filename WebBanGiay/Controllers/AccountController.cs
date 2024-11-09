using Microsoft.AspNetCore.Mvc;
using WebBanGiay.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Linq;

namespace WebBanGiay.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbwebGiayOnlineContext context;

        public AccountController(DbwebGiayOnlineContext context)
        {
            this.context = context;
        }

        // Hiển thị trang đăng nhập
        public IActionResult Login()
        {
            return View();
        }

        // Hiển thị trang đăng ký
        public IActionResult Register()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Tìm người dùng trong cơ sở dữ liệu
            var user = context.Customers.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email không tồn tại.");
                return View();
            }

            // Kiểm tra mật khẩu (so sánh với mật khẩu đã mã hóa)
            if (user.Password != HashPassword(password))
            {
                ModelState.AddModelError("", "Mật khẩu không đúng.");
                return View();
            }

            // Đăng nhập thành công, lưu thông tin vào session hoặc cookie
            // Ví dụ: TempData["User"] = user;
            return RedirectToAction("Home", "Home");
        }

        // Xử lý đăng ký
        [HttpPost]
        public IActionResult Register(Customer customer, string confirmPassword)
        {
            // Kiểm tra thông tin nhập vào
            if (customer.Password != confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu không khớp.");
                return View();
            }

            // Kiểm tra email đã tồn tại chưa
            if (context.Customers.Any(u => u.Email == customer.Email))
            {
                ModelState.AddModelError("", "Email đã tồn tại.");
                return View();
            }

            // Mã hóa mật khẩu trước khi lưu
            customer.Password = HashPassword(customer.Password);
            customer.CreatedAt = DateTime.Now;
            customer.Status = true; // mặc định là đã kích hoạt

            // Lưu thông tin người dùng vào cơ sở dữ liệu
            context.Customers.Add(customer);
            context.SaveChanges();

            // Đăng ký thành công, chuyển hướng về trang đăng nhập
            return RedirectToAction("Login");
        }

        // Hàm mã hóa mật khẩu (bạn có thể sử dụng phương pháp mã hóa khác như bcrypt)
        private string HashPassword(string password)
        {
            // Sử dụng phương pháp mã hóa cơ bản (KeyDerivation) cho mật khẩu
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 },
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
