using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class BaseSweater
{
    public int Id { get; set; }

    public string Settings { get; set; } = null!;

    public virtual ICollection<BaseSportSuit> BaseSportSuits { get; set; } = new List<BaseSportSuit>();

    public virtual ICollection<CustomSweater> CustomSweaters { get; set; } = new List<CustomSweater>();
}
