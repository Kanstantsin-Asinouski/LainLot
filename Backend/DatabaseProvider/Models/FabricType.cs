using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class FabricType
{
    public int Id { get; set; }

    public int FkCurrencies { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<CustomizableProduct> CustomizableProducts { get; set; } = new List<CustomizableProduct>();

    public virtual Currency FkCurrenciesNavigation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
