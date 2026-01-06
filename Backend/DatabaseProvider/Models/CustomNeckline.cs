using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class CustomNeckline
{
    public int Id { get; set; }

    public int FkBaseNecklines { get; set; }

    public string? CustomSettings { get; set; }

    public virtual ICollection<CustomSportSuit> CustomSportSuits { get; set; } = new List<CustomSportSuit>();

    public virtual BaseNeckline FkBaseNecklinesNavigation { get; set; } = null!;
}
