using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class CustomSleeve
{
    public int Id { get; set; }

    public int FkBaseSleeves { get; set; }

    public string? CustomSettings { get; set; }

    public virtual ICollection<CustomSportSuit> CustomSportSuits { get; set; } = new List<CustomSportSuit>();

    public virtual BaseSleeve FkBaseSleevesNavigation { get; set; } = null!;
}
