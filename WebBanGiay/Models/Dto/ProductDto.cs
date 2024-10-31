using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebBanGiay.Models.Dto
{
	public class ProductDto
	{
		// Shoe properties
		public int ShoeId { get; set; }
		public string? ShoeName { get; set; }
		public string? ShoeDescription { get; set; }
		public string? CareInstructions { get; set; }
		public int? BrandId { get; set; }
		public int? CategoryId { get; set; }

		// ShoeItem properties
		public List<ShoeItemDetail> ShoeItems { get; set; } = new List<ShoeItemDetail>();

		// Trường hình ảnh
		public List<ShoeImageDetail> ShoeImages { get; set; } = new List<ShoeImageDetail>();
		public List<string?> ImageUrls { get; set; } = new List<string?>();
	}

	public class ShoeItemDetail
	{
		public int ShoeItemId { get; set; }
		public int? ColourId { get; set; }
		public string? ColourName { get; set; }
		public int? SizeId { get; set; }
		public string? SizeName { get; set; }
		public decimal? Price { get; set; }
		public decimal? SalePrice { get; set; }
		public int? StockQuantity { get; set; }
		public string? Sku { get; set; }
	}
	public class ShoeImageDetail
	{
		public int ImageId { get; set; }
		public string? ImageUrl { get; set; }
	}
}