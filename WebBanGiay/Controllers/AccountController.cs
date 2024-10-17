using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebBanGiay.Controllers
{
    public class AccountController : Controller
    {
        //private DBDegreyStoreEntities db = new DBDegreyStoreEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    // Authentication succeeded, store user information in session

                    Session["Username"] = user.Username;
                    TempData["SuccessMessage"] = $"Chúc mừng, {user.Username}! Đăng nhập thành công.";
                    // Redirect to the specified page after successful login
                    return RedirectToAction("SanPham", "CustomerProduct", new { area = "" }); // Adjust the route based on your project structure

                    // You can also pass a success message to the view if needed
                    // TempData["SuccessMessage"] = "Login successful.";
                }
                else
                {
                    // Authentication failed, display error message
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                if (IsUniqueUsername(model.Username))
                {
                    // Registration succeeded, add the new user to the database
                    db.Users.Add(model);
                    db.SaveChanges();

                    // Set success message for registration
                    TempData["SuccessMessage"] = "Đăng kí thành công. Vui lòng đăng nhập.";

                    // Redirect to the login page
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    // Registration failed, username already exists, display error message
                    ModelState.AddModelError("", "Username already exists. Please choose a different username.");
                    return View(model);
                }
            }
            return View(model);
        }

        private bool IsUniqueUsername(string username)
        {
            // Kiểm tra xem tên đăng nhập đã tồn tại hay chưa
            return !db.Users.Any(u => u.Username == username);
        }
    }
}
