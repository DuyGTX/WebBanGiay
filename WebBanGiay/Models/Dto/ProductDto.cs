using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebBanGiay.Models.Dto
{
	public class ProductDto
	{
		public int ShoeId { get; set; }
		public int? CategoryId { get; set; }
		public int? BrandId { get; set; }
		public string? ShoeName { get; set; }
		public string? ShoeDescription { get; set; }
		public string? CareInstructions { get; set; }
		public string? Brand { get; set; }
		public string? Category { get; set; }
		public ICollection<ShoeImageDto> ShoeImages { get; set; }
		public ICollection<ShoeItemDto> ShoeItems { get; set; }
	}

	public class ShoeImageDto
	{
		public int ImageId { get; set; }
		public string? ImageUrl { get; set; }
	}

	public class ShoeItemDto
	{
		public int ShoeItemId { get; set; }
		public int? ColourId { get; set; }
		public int? SizeId { get; set; }
		public decimal? Price { get; set; }
		public decimal? SalePrice { get; set; }
		public int? StockQuantity { get; set; }
		public string? Sku { get; set; }
		public string? Colour { get; set; }
		public string? Size { get; set; }
	}
}