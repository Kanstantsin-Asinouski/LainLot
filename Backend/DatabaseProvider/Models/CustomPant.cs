using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class CustomPant
{
    public int Id { get; set; }

    public int FkBasePants { get; set; }

    public string? CustomSettings { get; set; }

    public virtual ICollection<CustomSportSuit> CustomSportSuits { get; set; } = new List<CustomSportSuit>();

    public virtual BasePant FkBasePantsNavigation { get; set; } = null!;
}
