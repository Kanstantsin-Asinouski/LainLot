using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public int FkAccessLevels { get; set; }

    public string Name { get; set; } = null!;

    public virtual AccessLevel FkAccessLevelsNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
