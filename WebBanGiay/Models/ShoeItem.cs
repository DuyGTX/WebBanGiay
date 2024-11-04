using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ShoeItem
{
    public int ShoeItemId { get; set; }

    public int ShoeId { get; set; }

    public decimal? Price { get; set; }

    public decimal? SalePrice { get; set; }

    public string? Sku { get; set; }

    public virtual Shoe? Shoe { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual ICollection<ShoeItemColour> ShoeItemColours { get; } = new List<ShoeItemColour>();

    public virtual ICollection<ShoeItemSize> ShoeItemSizes { get; } = new List<ShoeItemSize>();
}
