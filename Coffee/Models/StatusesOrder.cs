using System;
using System.Collections.Generic;

namespace Coffee.Models;

public partial class StatusesOrder
{
    public int IdStatus { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
