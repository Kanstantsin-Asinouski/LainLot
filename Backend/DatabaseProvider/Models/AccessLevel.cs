using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class AccessLevel
{
    public int Id { get; set; }

    public int Level { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
