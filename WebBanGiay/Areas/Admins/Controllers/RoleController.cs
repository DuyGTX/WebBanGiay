using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Models;

namespace WebBanGiay.Areas.Admins.Controllers
{
	[Area("Admins")]
	
	[Authorize(Roles = "Admin")]
	public class RoleController : Controller
	{
		private readonly DbwebGiayOnlineContext _dataContext;
		private readonly RoleManager<IdentityRole> _roleManager;
	
		public RoleController(DbwebGiayOnlineContext context, RoleManager<IdentityRole> roleManager) 
		{

			_dataContext = context;
			_roleManager = roleManager;
		}

		[Route("Index")]
		public async Task<IActionResult> Index(int pg = 1)
		{
			const int pageSize = 10; // Số mục mỗi trang

			if (pg < 1) pg = 1; // Đảm bảo số trang >= 1

			// Đếm tổng số mục
			int recsCount = await _dataContext.Roles.CountAsync();

			// Tạo đối tượng phân trang
			var pager = new Paginate(recsCount, pg, pageSize);

			// Xác định số mục cần bỏ qua
			int recSkip = (pg - 1) * pageSize;

			// Lấy danh sách roles đã phân trang
			var roles = await _dataContext.Roles
				.OrderByDescending(p => p.Id)
				.Skip(recSkip)
				.Take(pageSize)
				.ToListAsync();

			// Truyền dữ liệu phân trang vào View
			ViewBag.Pager = pager;

			return View(roles);
		}


		[HttpGet]
		[Route("Create")]
		public async Task<IActionResult> Create()
		{
			
			return View();
		}

		[HttpPost]
		[Route("Create")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(IdentityRole model)
		{
			if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
			}
			return Redirect("Index");
		}


		[HttpGet]
		[Route("Edit")]
		public async Task<IActionResult> Edit(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}
			var role = await _roleManager.FindByIdAsync(id);
			return View(role);
		}


		[HttpPost]
		[Route("Edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, IdentityRole model)
		{
			if (string.IsNullOrEmpty(id))
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				var role = await _roleManager.FindByIdAsync(id);
				if (role == null)
				{
					return NotFound();
				}
				role.Name = model.Name;
				try
				{
					await _roleManager.UpdateAsync(role);
					TempData["SuccessMessage"] = "Quyền đã được cập nhật thành công!";
					return RedirectToAction("Index");
				}
				catch (Exception ex) 
				{
					ModelState.AddModelError("", "Đã xuất hiện lỗi khi thực hiện cập nhật quyền.");
				}
				
			}
			return View(model ?? new IdentityRole { Id = id });
		}


		[HttpGet]
		[Route("Delete")]
		public async Task<IActionResult> Delete(string id)
		{
			if(string.IsNullOrEmpty(id))
			{
				return NotFound();
			}
			var role = await _roleManager.FindByIdAsync(id);
			if(role == null)
			{
				return NotFound();
			}
			try
			{
				await _roleManager.DeleteAsync(role);
				TempData["SuccessMessage"] = "Quyền đã được xóa thành công!";
			}
			catch(Exception ex)
			{
				ModelState.AddModelError("", "Đã xuất hiện lỗi khi thực hiện xóa quyền.");
			}
			
			return Redirect("Index");
		}

		


	}
}
