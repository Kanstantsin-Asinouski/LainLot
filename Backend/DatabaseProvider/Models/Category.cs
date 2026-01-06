using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class Category
{
    public int Id { get; set; }

    public int FkLanguages { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<CategoryHierarchy> CategoryHierarchies { get; set; } = new List<CategoryHierarchy>();

    public virtual Language FkLanguagesNavigation { get; set; } = null!;

    public virtual ICollection<ProductTranslation> ProductTranslations { get; set; } = new List<ProductTranslation>();
}
