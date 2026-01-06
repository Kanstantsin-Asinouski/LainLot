using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class OrderStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
