using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class UserOrderHistory
{
    public int Id { get; set; }

    public int FkOrders { get; set; }

    public int FkUsers { get; set; }

    public virtual Order FkOrdersNavigation { get; set; } = null!;

    public virtual User FkUsersNavigation { get; set; } = null!;
}
