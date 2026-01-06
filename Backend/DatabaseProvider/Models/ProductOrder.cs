using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class ProductOrder
{
    public int Id { get; set; }

    public int? FkProducts { get; set; }

    public int? FkCustomizableProducts { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual CustomizableProduct? FkCustomizableProductsNavigation { get; set; }

    public virtual Product? FkProductsNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
