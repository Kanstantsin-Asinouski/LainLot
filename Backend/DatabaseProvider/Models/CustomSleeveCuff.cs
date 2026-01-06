using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class CustomSleeveCuff
{
    public int Id { get; set; }

    public int FkBaseSleeveCuffs { get; set; }

    public string? CustomSettings { get; set; }

    public virtual ICollection<CustomSportSuit> CustomSportSuitFkCustomSleeveCuffsLeftNavigations { get; set; } = new List<CustomSportSuit>();

    public virtual ICollection<CustomSportSuit> CustomSportSuitFkCustomSleeveCuffsRightNavigations { get; set; } = new List<CustomSportSuit>();

    public virtual BaseSleeveCuff FkBaseSleeveCuffsNavigation { get; set; } = null!;
}
