using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Tên người dùng không được để trống")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Mật khẩu không được để trống")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ReturnUrl { get; set; } = "/";
	}
}
