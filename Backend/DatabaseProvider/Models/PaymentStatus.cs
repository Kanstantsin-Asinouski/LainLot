using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class PaymentStatus
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
