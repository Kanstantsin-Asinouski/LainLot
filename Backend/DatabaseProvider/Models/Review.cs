using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class Review
{
    public int Id { get; set; }

    public int FkProducts { get; set; }

    public int FkUsers { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Product FkProductsNavigation { get; set; } = null!;

    public virtual User FkUsersNavigation { get; set; } = null!;
}
