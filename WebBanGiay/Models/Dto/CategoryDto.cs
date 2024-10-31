
using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models.Dto

{
	public class CategoryDto
	{
		
		public int CategoryId { get; set; }

		[Required,MaxLength(100)]
		public string? CategoryName { get; set; } = " ";

		[Required]
		public int? BrandId { get; set; }// Thêm thuộc tính BrandName

	}
}
