using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class ProductTranslation
{
    public int Id { get; set; }

    public int FkLanguages { get; set; }

    public int FkProducts { get; set; }

    public int FkCategories { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual Category FkCategoriesNavigation { get; set; } = null!;

    public virtual Language FkLanguagesNavigation { get; set; } = null!;

    public virtual Product FkProductsNavigation { get; set; } = null!;
}
