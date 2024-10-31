using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ShoeItem
{
    public int ShoeItemId { get; set; }

    public int? ShoeId { get; set; }

    public int? ColourId { get; set; }

    public int? SizeId { get; set; }

    public decimal? Price { get; set; }

    public decimal? SalePrice { get; set; }

    public int? StockQuantity { get; set; }

    public string? Sku { get; set; }

    public virtual Colour? Colour { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual Shoe? Shoe { get; set; }

    public virtual Size? Size { get; set; }
}
