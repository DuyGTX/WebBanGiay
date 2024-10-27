using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? OrderId { get; set; }

    public int? ShoeItemId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ShoeItem? ShoeItem { get; set; }
}
