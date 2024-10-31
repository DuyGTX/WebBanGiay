using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string? BrandName { get; set; }

    public virtual ICollection<ShoeCategory> ShoeCategories { get; } = new List<ShoeCategory>();

    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
