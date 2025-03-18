using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanGiay.Models;

public partial class Shoe
{
    public int ShoeId { get; set; }

    public int? CategoryId { get; set; }

    public int? BrandId { get; set; }

    public int Quantity { get; set; }
    public int Sold { get; set; }
    public string? ShoeName { get; set; }

    public string? ShoeDescription { get; set; }

    public string? CareInstructions { get; set; }

    public decimal? Price { get; set; }

    public decimal? SalePrice { get; set; }

    public string? Sku { get; set; }

	[NotMapped] // Không ánh xạ vào database
	public List<string>? ImageUrls { get; set; }
	public virtual Brand? Brand { get; set; }

    public virtual ShoeCategory? Category { get; set; }

    public virtual ICollection<ShoeColour> ShoeColours { get; } = new List<ShoeColour>();

    public virtual ICollection<ShoeImage> ShoeImages { get; } = new List<ShoeImage>();

    public virtual ICollection<ShoeSize> ShoeSizes { get; } = new List<ShoeSize>();

}
