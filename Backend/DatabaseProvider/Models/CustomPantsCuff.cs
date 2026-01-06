using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class CustomPantsCuff
{
    public int Id { get; set; }

    public int FkBasePantCuffs { get; set; }

    public string? CustomSettings { get; set; }

    public virtual ICollection<CustomSportSuit> CustomSportSuitFkCustomPantsCuffsLeftNavigations { get; set; } = new List<CustomSportSuit>();

    public virtual ICollection<CustomSportSuit> CustomSportSuitFkCustomPantsCuffsRightNavigations { get; set; } = new List<CustomSportSuit>();

    public virtual BasePantsCuff FkBasePantCuffsNavigation { get; set; } = null!;
}
