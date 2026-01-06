using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class CategoryHierarchy
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public int FkCategories { get; set; }

    public virtual Category FkCategoriesNavigation { get; set; } = null!;

    public virtual ICollection<CategoryHierarchy> InverseParent { get; set; } = new List<CategoryHierarchy>();

    public virtual CategoryHierarchy? Parent { get; set; }
}
