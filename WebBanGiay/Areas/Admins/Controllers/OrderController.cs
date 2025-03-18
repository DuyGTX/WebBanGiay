using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Models;

namespace WebBanGiay.Areas.Admins.Controllers
{
    [Area("Admins")]
	[Authorize(Roles = "Admin, Employee")]
	public class OrderController : Controller
    {
        private readonly DbwebGiayOnlineContext context;

        public OrderController(DbwebGiayOnlineContext context)
        {
            this.context = context;
        }
		public async Task<IActionResult> Index(int pg = 1)
		{
			const int pageSize = 10; // Số mục mỗi trang

			if (pg < 1) pg = 1; // Đảm bảo số trang >= 1

			// Đếm tổng số mục
			int recsCount = await context.Orders.CountAsync();

			// Tạo đối tượng phân trang
			var pager = new Paginate(recsCount, pg, pageSize);

			// Xác định số mục cần bỏ qua
			int recSkip = (pg - 1) * pageSize;

			// Lấy danh sách orders đã phân trang
			var orders = await context.Orders
				.OrderByDescending(p => p.Id)
				.Skip(recSkip)
				.Take(pageSize)
				.ToListAsync();

			// Truyền dữ liệu phân trang vào View
			ViewBag.Pager = pager;

			return View(orders);
		}

		[HttpGet]
        [Route("View")]
        public async Task<IActionResult> View(string ordercode)
        {
            var DetailOrder = await context.OrderDetails.Include(s => s.shoe).Where(s => s.OrderCode == ordercode).ToListAsync();
            return View(DetailOrder);
        }
		[HttpPost]
		[Route("UpdateOrder")]
		public async Task<IActionResult> UpdateOrder(List<string> ordercodes, int status)
		{
			if (ordercodes == null || !ordercodes.Any())
			{
				return BadRequest(new { success = false, message = "Không có mã đơn hàng nào được cung cấp." });
			}

			var orders = await context.Orders.Where(o => ordercodes.Contains(o.OrderCode)).ToListAsync();
			if (!orders.Any())
			{
				return NotFound(new { success = false, message = "Không tìm thấy đơn hàng nào." });
			}

			foreach (var order in orders)
			{
				order.Status = status;
			}

			try
			{
				await context.SaveChangesAsync();
				// Trả về URL để chuyển hướng sau khi cập nhật thành công
				return Ok(new { success = true, message = "Trạng thái đơn hàng đã cập nhật thành công.", redirectUrl = Url.Action("Index", "Order", new { area = "Admins" }) });
			}
			catch (Exception)
			{
				return StatusCode(500, new { success = false, message = "Đã xuất hiện lỗi khi cập nhật trạng thái đơn hàng." });
			}
		}


		[HttpPost]
		[Route("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(string ordercode)
		{
			// Kiểm tra sự tồn tại của đơn hàng dựa trên ordercode
			var order = await context.Orders.FirstOrDefaultAsync(o => o.OrderCode == ordercode);
			if (order == null)
			{
				TempData["ErrorMessage"] = "Không tìm thấy đơn hàng.";
				return RedirectToAction("Index", "Order", new { area = "Admins" });
			}

			// Xóa chi tiết đơn hàng trước (nếu có)
			var orderDetails = await context.OrderDetails.Where(od => od.OrderCode == ordercode).ToListAsync();
			context.OrderDetails.RemoveRange(orderDetails);

			// Xóa đơn hàng
			context.Orders.Remove(order);

			try
			{
				await context.SaveChangesAsync();
				TempData["SuccessMessage"] = "Đơn hàng đã được xóa thành công.";
				return RedirectToAction("Index", "Order", new { area = "Admins" });
			}
			catch (Exception)
			{
				TempData["ErrorMessage"] = "Đã xảy ra lỗi khi xóa đơn hàng.";
				return RedirectToAction("Index", "Order", new { area = "Admins" });
			}
		}


	}
}
