using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class BaseSleeveCuff
{
    public int Id { get; set; }

    public string Settings { get; set; } = null!;

    public virtual ICollection<BaseSportSuit> BaseSportSuitFkBaseSleeveCuffsLeftNavigations { get; set; } = new List<BaseSportSuit>();

    public virtual ICollection<BaseSportSuit> BaseSportSuitFkBaseSleeveCuffsRightNavigations { get; set; } = new List<BaseSportSuit>();

    public virtual ICollection<CustomSleeveCuff> CustomSleeveCuffs { get; set; } = new List<CustomSleeveCuff>();
}
