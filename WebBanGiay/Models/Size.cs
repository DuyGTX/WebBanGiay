using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Size
{
    public int SizeId { get; set; }

    public string? SizeName { get; set; }

    public virtual ICollection<ShoeItemSize> ShoeItemSizes { get; } = new List<ShoeItemSize>();
}
