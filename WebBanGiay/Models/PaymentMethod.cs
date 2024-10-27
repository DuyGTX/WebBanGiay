using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class PaymentMethod
{
    public int PaymentMethodId { get; set; }

    public string MethodName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
