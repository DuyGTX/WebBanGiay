using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models
{
	public class ProductQuantityModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage ="Không được bỏ trống số lượng")]
		public int Quantity { get; set; }
		public int ShoeId { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
