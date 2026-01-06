using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class CustomSportSuit
{
    public int Id { get; set; }

    public int? FkCustomNecklines { get; set; }

    public int? FkCustomSweaters { get; set; }

    public int? FkCustomSleeves { get; set; }

    public int? FkCustomSleeveCuffsLeft { get; set; }

    public int? FkCustomSleeveCuffsRight { get; set; }

    public int? FkCustomBelts { get; set; }

    public int? FkCustomPants { get; set; }

    public int? FkCustomPantsCuffsLeft { get; set; }

    public int? FkCustomPantsCuffsRight { get; set; }

    public virtual ICollection<CustomizableProduct> CustomizableProducts { get; set; } = new List<CustomizableProduct>();

    public virtual CustomBelt? FkCustomBeltsNavigation { get; set; }

    public virtual CustomNeckline? FkCustomNecklinesNavigation { get; set; }

    public virtual CustomPantsCuff? FkCustomPantsCuffsLeftNavigation { get; set; }

    public virtual CustomPantsCuff? FkCustomPantsCuffsRightNavigation { get; set; }

    public virtual CustomPant? FkCustomPantsNavigation { get; set; }

    public virtual CustomSleeveCuff? FkCustomSleeveCuffsLeftNavigation { get; set; }

    public virtual CustomSleeveCuff? FkCustomSleeveCuffsRightNavigation { get; set; }

    public virtual CustomSleeve? FkCustomSleevesNavigation { get; set; }

    public virtual CustomSweater? FkCustomSweatersNavigation { get; set; }
}
