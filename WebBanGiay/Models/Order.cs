using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? PaymentMethodId { get; set; }

    public int? StatusId { get; set; }

    public int? ShippingAddressId { get; set; }

    public int? CouponId { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual ICollection<OrderNote> OrderNotes { get; } = new List<OrderNote>();

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual ShippingAddress? ShippingAddress { get; set; }

    public virtual OrderStatus? Status { get; set; }
}
