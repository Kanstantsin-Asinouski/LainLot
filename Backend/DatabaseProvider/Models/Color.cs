using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class Color
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public byte[] ImageData { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
