using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int FkProductOrders { get; set; }

    public int FkCurrencies { get; set; }

    public decimal Price { get; set; }

    public int Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Currency FkCurrenciesNavigation { get; set; } = null!;

    public virtual ProductOrder FkProductOrdersNavigation { get; set; } = null!;
}
