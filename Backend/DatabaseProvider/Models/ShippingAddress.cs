using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class ShippingAddress
{
    public int Id { get; set; }

    public int FkCountries { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string ZipPostCode { get; set; } = null!;

    public string StateProvince { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual Country FkCountriesNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
