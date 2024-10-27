using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ShippingAddress
{
    public int AddressId { get; set; }

    public int? CustomerId { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
