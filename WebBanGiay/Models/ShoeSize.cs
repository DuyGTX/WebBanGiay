using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ShoeSize
{
    public int ShoeId { get; set; }

    public int SizeId { get; set; }

    public int? StockQuantity { get; set; }

    public virtual Shoe Shoe { get; set; } = null!;

    public virtual Size Size { get; set; } = null!;
}
