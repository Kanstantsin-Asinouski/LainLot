using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class CustomizableProduct
{
    public int Id { get; set; }

    public int FkCustomSportSuits { get; set; }

    public int FkFabricTypes { get; set; }

    public int FkSizeOptions { get; set; }

    public decimal Price { get; set; }

    public string? CustomizationDetails { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual CustomSportSuit FkCustomSportSuitsNavigation { get; set; } = null!;

    public virtual FabricType FkFabricTypesNavigation { get; set; } = null!;

    public virtual SizeOption FkSizeOptionsNavigation { get; set; } = null!;

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
}
