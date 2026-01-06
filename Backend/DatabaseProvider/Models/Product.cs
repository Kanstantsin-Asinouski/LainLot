using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class Product
{
    public int Id { get; set; }

    public int FkFabricTypes { get; set; }

    public int FkColors { get; set; }

    public int FkSizeOptions { get; set; }

    public int FkCurrencies { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public bool IsActive { get; set; }

    public bool IsCustomizable { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Color FkColorsNavigation { get; set; } = null!;

    public virtual Currency FkCurrenciesNavigation { get; set; } = null!;

    public virtual FabricType FkFabricTypesNavigation { get; set; } = null!;

    public virtual SizeOption FkSizeOptionsNavigation { get; set; } = null!;

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public virtual ICollection<ProductTranslation> ProductTranslations { get; set; } = new List<ProductTranslation>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
