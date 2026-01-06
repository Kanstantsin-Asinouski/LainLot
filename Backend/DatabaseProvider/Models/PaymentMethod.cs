using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string? Method { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
