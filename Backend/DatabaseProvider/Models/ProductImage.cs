using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class ProductImage
{
    public int Id { get; set; }

    public int FkProducts { get; set; }

    public byte[] ImageData { get; set; } = null!;

    public virtual Product FkProductsNavigation { get; set; } = null!;
}
