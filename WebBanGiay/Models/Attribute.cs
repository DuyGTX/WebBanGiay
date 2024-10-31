using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class Attribute
{
    public int AttributeId { get; set; }

    public string? AttributeName { get; set; }

    public virtual ICollection<ShoeAttribute> ShoeAttributes { get; } = new List<ShoeAttribute>();
}
