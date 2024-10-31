using System;
using System.Collections.Generic;

namespace WebBanGiay.Models;

public partial class OrderNote
{
    public int NoteId { get; set; }

    public int? OrderId { get; set; }

    public string? NoteText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Order? Order { get; set; }
}
