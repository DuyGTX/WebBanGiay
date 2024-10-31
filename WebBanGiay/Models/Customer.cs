using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? CusAddress { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<RoleAccount> RoleAccounts { get; } = new List<RoleAccount>();

    public virtual ICollection<ShippingAddress> ShippingAddresses { get; } = new List<ShippingAddress>();
}
