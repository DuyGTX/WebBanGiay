using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models
{
	public class UserModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Tên tài khoản không được để trống")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Email không được để trống")]
		[EmailAddress(ErrorMessage = "Email không hợp lệ")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Mật khẩu không được để trống")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
		public string ConfirmPassword { get; set; }

	}
}
