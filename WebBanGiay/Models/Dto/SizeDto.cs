using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models.Dto
{
	public class SizeDto
	{
		public int SizeId { get; set; }

		[Required,MaxLength(10)]
		public string? SizeName { get; set; } = "";

		
	}
}
