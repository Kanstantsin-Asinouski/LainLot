using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class BasePantsCuff
{
    public int Id { get; set; }

    public string Settings { get; set; } = null!;

    public virtual ICollection<BaseSportSuit> BaseSportSuitFkBasePantsCuffsLeftNavigations { get; set; } = new List<BaseSportSuit>();

    public virtual ICollection<BaseSportSuit> BaseSportSuitFkBasePantsCuffsRightNavigations { get; set; } = new List<BaseSportSuit>();

    public virtual ICollection<CustomPantsCuff> CustomPantsCuffs { get; set; } = new List<CustomPantsCuff>();
}
