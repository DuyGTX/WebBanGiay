using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Coupon
{
    public int CouponId { get; set; }

    public string? CouponCode { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public bool? IsActive { get; set; }

    
}
