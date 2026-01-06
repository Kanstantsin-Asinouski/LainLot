using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class CustomSweater
{
    public int Id { get; set; }

    public int FkBaseSweaters { get; set; }

    public string? CustomSettings { get; set; }

    public virtual ICollection<CustomSportSuit> CustomSportSuits { get; set; } = new List<CustomSportSuit>();

    public virtual BaseSweater FkBaseSweatersNavigation { get; set; } = null!;
}
