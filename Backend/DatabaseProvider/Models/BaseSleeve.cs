using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class BaseSleeve
{
    public int Id { get; set; }

    public string Settings { get; set; } = null!;

    public virtual ICollection<BaseSportSuit> BaseSportSuits { get; set; } = new List<BaseSportSuit>();

    public virtual ICollection<CustomSleeve> CustomSleeves { get; set; } = new List<CustomSleeve>();
}
