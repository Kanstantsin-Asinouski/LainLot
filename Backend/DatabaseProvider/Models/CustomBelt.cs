using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class CustomBelt
{
    public int Id { get; set; }

    public int FkBaseBelts { get; set; }

    public string? CustomSettings { get; set; }

    public virtual ICollection<CustomSportSuit> CustomSportSuits { get; set; } = new List<CustomSportSuit>();

    public virtual BaseBelt FkBaseBeltsNavigation { get; set; } = null!;
}
