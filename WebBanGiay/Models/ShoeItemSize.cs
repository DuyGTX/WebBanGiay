using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ShoeItemSize
{
    public int ShoeItemId { get; set; }

    public int SizeId { get; set; }

    public int? StockQuantity { get; set; }

    public virtual ShoeItem ShoeItem { get; set; } = null!;

    public virtual Size Size { get; set; } = null!;
}
