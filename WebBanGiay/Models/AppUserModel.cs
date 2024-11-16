using Microsoft.AspNetCore.Identity;

namespace WebBanGiay.Models
{
	public class AppUserModel : IdentityUser
	{
		public string? Occupation {get; set;}
	}
}
