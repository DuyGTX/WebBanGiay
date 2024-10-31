

using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models.Dto
{
	public class ColourDto
	{
		public int ColourId { get; set; }
		[Required, MaxLength(100)]
		public string? ColourName { get; set; } = "";
	}
}
