using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models.Dto
{
	public class BrandDto
	{
		public int BrandId { get; set; }

		[Required, MaxLength(100)]
		public string? BrandName { get; set; } = " ";
		
	}
}
