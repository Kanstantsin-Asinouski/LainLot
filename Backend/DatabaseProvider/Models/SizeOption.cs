using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class SizeOption
{
    public int Id { get; set; }

    public string Size { get; set; } = null!;

    public virtual ICollection<CustomizableProduct> CustomizableProducts { get; set; } = new List<CustomizableProduct>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
