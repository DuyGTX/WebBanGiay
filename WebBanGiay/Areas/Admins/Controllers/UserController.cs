// Import các namespace cần thiết
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Models;

namespace WebBanGiay.Areas.Admins.Controllers
{
	[Area("Admins")]
	[Route("Admins/User")]
	[Authorize(Roles = "Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<AppUserModel> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		private readonly DbwebGiayOnlineContext _datacontext;

		public UserController(DbwebGiayOnlineContext context,UserManager<AppUserModel> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_datacontext = context;

		}

		[HttpGet]
		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			var usersWithRoles = await (from u in _datacontext.Users
										join ur in _datacontext.UserRoles on u.Id equals ur.UserId
										join r in _datacontext.Roles on ur.RoleId equals r.Id
										select new { User = u, RoleName = r.Name }).ToListAsync();

			return View(usersWithRoles);
		}


		[HttpGet]
		[Route("Create")]
		public async Task<IActionResult> Create()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			ViewBag.Roles = new SelectList(roles, "Id", "Name");
			return View(new AppUserModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("Create")]
		public async Task<IActionResult> Create(AppUserModel user)
		{
			if (ModelState.IsValid)
			{
				var createUserResult = await _userManager.CreateAsync(user, user.PasswordHash); // Tạo user
				if (createUserResult.Succeeded)
				{
					var createUser = await _userManager.FindByEmailAsync(user.Email); // Tìm user dựa vào Email
					if (createUser == null)
					{
						ModelState.AddModelError(string.Empty, "User not found!");
						return View(user);
					}

					// Kiểm tra RoleId không null hoặc trống
					if (string.IsNullOrEmpty(user.RoleId))
					{
						ModelState.AddModelError(string.Empty, "Role ID cannot be null or empty!");
						return View(user);
					}

					var role = await _roleManager.FindByIdAsync(user.RoleId); // Lấy RoleId
					if (role == null)
					{
						ModelState.AddModelError(string.Empty, "Role not found!");
						return View(user);
					}

					var addToRoleResult = await _userManager.AddToRoleAsync(createUser, role.Name); // Gán quyền
					if (addToRoleResult.Succeeded)
					{
						return RedirectToAction("Index", "User");
					}
					else
					{
						foreach (var error in addToRoleResult.Errors)
						{
							ModelState.AddModelError(string.Empty, error.Description);
						}
						return View(user);
					}
				}
				else
				{
					foreach (var error in createUserResult.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return View(user);
				}
			}
			else
			{
				TempData["ErrorMessage"] = "Model có vài thứ đang lỗi";
				var roles = await _roleManager.Roles.ToListAsync();
				ViewBag.Roles = new SelectList(roles, "Id", "Name");
				var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
				string erroMessage = string.Join("\n", errors);
				return View(user);
			}
		}

		[HttpGet]
		[Route("Delete")]
		public async Task<IActionResult> Delete(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			var deleteResult = await _userManager.DeleteAsync(user);
			if (!deleteResult.Succeeded)
			{
				return NotFound();
			}
			TempData["SuccessMessage"] = "User đã được xóa thành công";
			return RedirectToAction("Index");
		}

		[HttpGet]
		[Route("Edit")]
		public async Task<IActionResult> Edit(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			var roles = await _roleManager.Roles.ToListAsync();
			ViewBag.Roles = new SelectList(roles, "Id", "Name");
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("Edit")]
		public async Task<IActionResult> Edit(string id, AppUserModel user)
		{
			var existingUser = await _userManager.FindByIdAsync(id);
			if (existingUser == null)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				existingUser.UserName = user.UserName;
				existingUser.Email = user.Email;
				existingUser.PhoneNumber = user.PhoneNumber;
				existingUser.RoleId = user.RoleId;

				var updateUserResult = await _userManager.UpdateAsync(existingUser);
				if (updateUserResult.Succeeded)
				{
					var userRoles = await _userManager.GetRolesAsync(existingUser);
					var removeRolesResult = await _userManager.RemoveFromRolesAsync(existingUser, userRoles);
					if (removeRolesResult.Succeeded)
					{
						var newRole = await _roleManager.FindByIdAsync(user.RoleId);
						if (newRole != null)
						{
							var addToRoleResult = await _userManager.AddToRoleAsync(existingUser, newRole.Name);
							if (addToRoleResult.Succeeded)
							{
								return RedirectToAction("Index", "User");
							}
							else
							{
								AddIdentityErrors(addToRoleResult);
							}
						}
					}
					else
					{
						AddIdentityErrors(removeRolesResult);
					}
				}
				else
				{
					AddIdentityErrors(updateUserResult);
				}
			}
			var roles = await _roleManager.Roles.ToListAsync();
			ViewBag.Roles = new SelectList(roles, "Id", "Name");
			TempData["ErrorMessage"] = "Model có vài thứ đang lỗi";
			var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
			string erroMessage = string.Join("\n", errors);
			return View(existingUser);
		}

		private void AddIdentityErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
		}
	}
}
