using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class Order
{
    public int Id { get; set; }

    public int FkProductOrders { get; set; }

    public int FkOrderStatus { get; set; }

    public int FkPayments { get; set; }

    public int FkShippingAddresses { get; set; }

    public decimal Price { get; set; }

    public int Amount { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual OrderStatus FkOrderStatusNavigation { get; set; } = null!;

    public virtual Payment FkPaymentsNavigation { get; set; } = null!;

    public virtual ProductOrder FkProductOrdersNavigation { get; set; } = null!;

    public virtual ShippingAddress FkShippingAddressesNavigation { get; set; } = null!;

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    public virtual ICollection<UserOrderHistory> UserOrderHistories { get; set; } = new List<UserOrderHistory>();
}
