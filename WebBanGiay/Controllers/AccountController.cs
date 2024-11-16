using Microsoft.AspNetCore.Mvc;
using WebBanGiay.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using WebBanGiay.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace WebBanGiay.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUserModel> _userManager;
		private SignInManager<AppUserModel> _signInManager;


		public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Login(string returnUrl = "/")
		{
			ViewBag.ReturnUrl = returnUrl;
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}




		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM, string returnUrl = "/")
		{
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
				if (result.Succeeded)
				{
					return Redirect(returnUrl);
				}
				ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác");
			}
			// Đảm bảo ViewBag có giá trị ReturnUrl khi tải lại View
			ViewBag.ReturnUrl = returnUrl;
			return View(loginVM);
		}


		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UserModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// Kiểm tra xem tên người dùng đã tồn tại chưa
			var isUserNameTaken = await _userManager.Users.AnyAsync(u => u.UserName == model.UserName);
			if (isUserNameTaken)
			{
				ModelState.AddModelError("UserName", $"Tên người dùng '{model.UserName}' đã được sử dụng.");
			}

			// Kiểm tra xem email đã tồn tại chưa
			var isEmailTaken = await _userManager.Users.AnyAsync(u => u.Email == model.Email);
			if (isEmailTaken)
			{
				ModelState.AddModelError("Email", "Email này đã được đăng ký.");
			}

			// Nếu có lỗi, trả về view với thông báo lỗi
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			// Sử dụng AppUserModel thay vì IdentityUser
			var user = new AppUserModel
			{
				UserName = model.UserName,
				Email = model.Email
			};

			// Tạo người dùng với UserManager sử dụng AppUserModel
			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				// Đăng nhập người dùng sau khi đăng ký thành công (nếu cần)
				await _signInManager.SignInAsync(user, isPersistent: false);
				TempData["SuccessMessage"] = "Tạo tài khoản thành công.";
				return RedirectToAction("Login", "Account");
			}

			// Thêm lỗi nếu quá trình đăng ký không thành công
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}
        public async Task<IActionResult> Logout(string returnUrl = "/")
		{
			await _signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}




    }
}
