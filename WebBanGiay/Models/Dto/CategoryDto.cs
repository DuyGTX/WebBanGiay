
using System.ComponentModel.DataAnnotations;

namespace WebBanGiay.Models.Dto

{
	public class CategoryDto
	{
		[Required,MaxLength(100)]
		public string? CategoryName { get; set; } = " ";
	}
}
