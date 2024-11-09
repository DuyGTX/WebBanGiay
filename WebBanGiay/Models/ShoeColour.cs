using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ShoeColour
{
    public int ShoeId { get; set; }

    public int ColourId { get; set; }

    public int? StockQuantity { get; set; }

    public virtual Colour Colour { get; set; } = null!;

    public virtual Shoe Shoe { get; set; } = null!;
}
