using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int FkPaymentMethods { get; set; }

    public int FkCurrencies { get; set; }

    public int FkPaymentStatuses { get; set; }

    public decimal Price { get; set; }

    public DateTime PaymentDate { get; set; }

    public string? PaymentNumber { get; set; }

    public virtual Currency FkCurrenciesNavigation { get; set; } = null!;

    public virtual PaymentMethod FkPaymentMethodsNavigation { get; set; } = null!;

    public virtual PaymentStatus FkPaymentStatusesNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
