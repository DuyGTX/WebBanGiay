using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ShoeItemColour
{
    public int ShoeItemId { get; set; }

    public int ColourId { get; set; }

    public int? StockQuantity { get; set; }

    public virtual Colour Colour { get; set; } = null!;

    public virtual ShoeItem ShoeItem { get; set; } = null!;
}
