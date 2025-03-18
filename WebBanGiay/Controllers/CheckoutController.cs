using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebBanGiay.Areas.Admins.Repository;
using WebBanGiay.Models;
using WebBanGiay.Models.ViewModels;
using WebBanGiay.Repository;

namespace WebBanGiay.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DbwebGiayOnlineContext _dataContext;
		private readonly IEmailSender _emailSender;
		private readonly ILogger<CheckoutController> _logger;
		public CheckoutController(DbwebGiayOnlineContext context, IEmailSender emailSender, ILogger<CheckoutController> logger)
        {
            _dataContext = context;
			_emailSender = emailSender;
			_logger = logger;

		}

        public async Task<IActionResult> Checkout()
        {
            try
            {
                // Lấy email người dùng
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(userEmail))
                {
                    return RedirectToAction("Login", "Account");
                }

                var orderCode = Guid.NewGuid().ToString();

                var orderItem = new OrderModel
                {
                    OrderCode = orderCode,
                    UserName = userEmail,
                    Status = 1,
                    CreatedDate = DateTime.Now
                };

                List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                if (!cartItems.Any())
                {
                    TempData["ErrorMessage"] = "Giỏ hàng trống. Không thể đặt hàng.";
                    return RedirectToAction("Index", "Cart");
                }

                using (var transaction = await _dataContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Thêm đơn hàng
                        _dataContext.Add(orderItem);
                        await _dataContext.SaveChangesAsync();

                        // Thêm chi tiết đơn hàng
                        var orderDetails = cartItems.Select(cart => new OrderDetail
                        {
                            UserName = userEmail,
                            OrderCode = orderCode,
                            ShoeId = cart.ShoeId,
                            Price = cart.Price,
                            Quantity = cart.Quantity
                        }).ToList();

                        _dataContext.AddRange(orderDetails);
                        await _dataContext.SaveChangesAsync();

                        // Cập nhật số lượng tồn kho và số lượng bán ra
                        foreach (var cart in cartItems)
                        {
                            var shoe = await _dataContext.Shoes.FirstOrDefaultAsync(s => s.ShoeId == cart.ShoeId);
                            if (shoe != null)
                            {
                                // Giảm tồn kho và tăng số lượng bán
                                shoe.Quantity -= cart.Quantity ?? 0;
                                shoe.Sold += cart.Quantity ?? 0;

                                // Kiểm tra số lượng âm
                                if (shoe.Quantity < 0)
                                {
                                    TempData["ErrorMessage"] = $"Sản phẩm {shoe.ShoeName} không đủ hàng trong kho.";
                                    return RedirectToAction("Index", "Cart");
                                }

                                _dataContext.Update(shoe);
                            }
                        }

                        await _dataContext.SaveChangesAsync();

                        // Commit transaction
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

                // Xóa giỏ hàng
                HttpContext.Session.Remove("Cart");

                // Chuẩn bị nội dung email
                var orderDetailsText = cartItems.Select(cart =>
					$"Sản phẩm: {cart.ShoeName}, Số lượng: {cart.Quantity}, Giá: {cart.Price:N0} VNĐ").ToList();

				var totalAmount = cartItems.Sum(c => c.Price * c.Quantity);

				var messageBody = $@"
<!DOCTYPE html>
<html lang='vi'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
        .email-container {{ background-color: #ffffff; max-width: 600px; margin: 20px auto; border-radius: 10px; overflow: hidden; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); }}
        .email-header {{ background-color: #007bff; color: #ffffff; padding: 20px; text-align: center; }}
        .email-header h1 {{ margin: 0; font-size: 24px; }}
        .email-body {{ padding: 20px; line-height: 1.6; color: #333333; }}
        .email-body h2 {{ color: #007bff; margin-bottom: 10px; }}
        .email-body ul {{ padding: 0; margin: 0 0 20px; list-style-type: none; }}
        .email-body ul li {{ padding: 10px; background-color: #f9f9f9; border: 1px solid #e9e9e9; border-radius: 5px; margin-bottom: 5px; }}
        .email-footer {{ background-color: #f4f4f4; text-align: center; padding: 10px; font-size: 14px; color: #888888; }}
        .email-footer a {{ color: #007bff; text-decoration: none; }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='email-header'>
            <h1>Xác Nhận Đơn Hàng</h1>
        </div>
        <div class='email-body'>
            <p>Mã đơn hàng: <strong>{orderCode}</strong></p>
            <h2>Chi Tiết Đơn Hàng:</h2>
            <ul>
                {string.Join("", orderDetailsText.Select(detail => $"<li>{detail}</li>"))}
            </ul>
            <p><strong>Tổng giá trị:</strong> {totalAmount:N0} VNĐ</p>
            <p>Cảm ơn bạn đã mua hàng tại <strong>T1 Shop</strong>! Chúng tôi sẽ sớm liên hệ để xác nhận.</p>
        </div>
        <div class='email-footer'>
            <p>© 2024 T1 Shop. Mọi bản quyền thuộc về Nhóm 15.</p>
            <p><a href='https://www.t1shop.com'>Truy cập cửa hàng</a></p>
        </div>
    </div>
</body>
</html>";


				// Gửi email
				try
				{
					await _emailSender.SendEmailAsync(
						userEmail,
						"Đặt hàng thành công",
						messageBody,
						isHtml: true
					);
				}
				catch (Exception emailEx)
				{
					// Ghi log lỗi email nhưng không ngừng xử lý
					_logger.LogError(emailEx, "Lỗi gửi email xác nhận đơn hàng");
				}

				// Thông báo thành công
				TempData["SuccessMessage"] = "Đặt hàng thành công, vui lòng đợi xác nhận đơn hàng";
				return RedirectToAction("Index", "Cart");
			}
			catch (Exception ex)
			{
				// Ghi log lỗi chung
				_logger.LogError(ex, "Lỗi trong quá trình đặt hàng");

				// Thông báo lỗi cho người dùng
				TempData["ErrorMessage"] = "Có lỗi xảy ra. Vui lòng thử lại sau.";
				return RedirectToAction("Index", "Cart");
			}
		}
	}
}
