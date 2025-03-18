using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models.Dto
{
	public class ShoeSizeDto
	{
		public int ShoeId { get; set; }


		public string? SizeName { get; set; }
		public int SizeId { get; set; }

		public int? StockQuantity { get; set; }

		public virtual Shoe Shoe { get; set; } = null!;

		public virtual Size Size { get; set; } = null!;
	}
}