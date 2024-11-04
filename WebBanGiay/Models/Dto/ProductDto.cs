using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebBanGiay.Models.Dto
{
	public class ProductDto
	{
        public int ShoeId { get; set; }
        public string? ShoeName { get; set; }
        public string? ShoeDescription { get; set; }
        public string? CareInstructions { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        public List<ShoeItemDetail> ShoeItems { get; set; } = new List<ShoeItemDetail>();
        public List<ShoeImageDetail> ShoeImages { get; set; } = new List<ShoeImageDetail>();
        public List<string?> ImageUrls { get; set; } = new List<string?>();
    }

	public class ShoeItemDetail
	{
		public int ShoeItemId { get; set; }
        // Thay đổi từ single colour sang list of colours
        public int? SizeId { get; set; }
        public List<ColourDetail> Colours { get; set; } = new List<ColourDetail>();
        public List<SizeDetail> Sizes { get; set; } = new List<SizeDetail>();
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string? Sku { get; set; }
	}
	public class ShoeImageDetail
	{
		public int ImageId { get; set; }
		public string? ImageUrl { get; set; }
	}
    public class ColourDetail
    {
        public int ColourId { get; set; }
        public string? ColourName { get; set; }
        public int? StockQuantity { get; set; }
    }

    public class SizeDetail
    {
        public int SizeId { get; set; }
        public string? SizeName { get; set; }
        public int? StockQuantity { get; set; }
    }
}