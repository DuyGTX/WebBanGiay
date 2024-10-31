using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ShoeImage
{
    public int ImageId { get; set; }

    public int? ShoeId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Shoe? Shoe { get; set; }
}
