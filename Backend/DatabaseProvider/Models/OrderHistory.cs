using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class OrderHistory
{
    public int Id { get; set; }

    public int FkOrders { get; set; }

    public int FkOrderStatuses { get; set; }

    public DateTime ChangedAt { get; set; }

    public virtual OrderStatus FkOrderStatusesNavigation { get; set; } = null!;

    public virtual Order FkOrdersNavigation { get; set; } = null!;
}
