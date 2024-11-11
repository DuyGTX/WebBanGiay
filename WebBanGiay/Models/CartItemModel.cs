using WebBanGiay.Areas.Admins.Controllers;

namespace WebBanGiay.Models
{
    public class CartItemModel
    {
        public int ShoeId { get; set; }
        public string? ShoeName { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total
        {
            get { return Quantity * Price; }
        }
        public string? ImageUrl { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();

        public CartItemModel()
        {
        }

        public CartItemModel(Shoe product)
        {
            ShoeId = product.ShoeId;
            ShoeName = product.ShoeName;
            Price = product.Price;
            Quantity = 1;

            // Chỉ lấy hình ảnh đầu tiên
            if (product.ShoeImages != null && product.ShoeImages.Any())
            {
                ImageUrl = product.ShoeImages.FirstOrDefault()?.ImageUrl;
            }
        }
    }
}
