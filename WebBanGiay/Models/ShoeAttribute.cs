using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class ShoeAttribute
{
    public int ShoeId { get; set; }

    public int AttributeId { get; set; }

    public string? AttributeValue { get; set; }

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual Shoe Shoe { get; set; } = null!;
}
