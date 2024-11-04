using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Colour
{
    public int ColourId { get; set; }

    public string? ColourName { get; set; }

    public virtual ICollection<ShoeItemColour> ShoeItemColours { get; } = new List<ShoeItemColour>();
}
