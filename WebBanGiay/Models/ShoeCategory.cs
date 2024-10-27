using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ShoeCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public int? BrandId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
