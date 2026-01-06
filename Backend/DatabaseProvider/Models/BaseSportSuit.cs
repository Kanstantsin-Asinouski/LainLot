using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class BaseSportSuit
{
    public int Id { get; set; }

    public int? FkBaseNecklines { get; set; }

    public int? FkBaseSweaters { get; set; }

    public int? FkBaseSleeves { get; set; }

    public int? FkBaseSleeveCuffsLeft { get; set; }

    public int? FkBaseSleeveCuffsRight { get; set; }

    public int? FkBaseBelts { get; set; }

    public int? FkBasePants { get; set; }

    public int? FkBasePantsCuffsLeft { get; set; }

    public int? FkBasePantsCuffsRight { get; set; }

    public virtual BaseBelt? FkBaseBeltsNavigation { get; set; }

    public virtual BaseNeckline? FkBaseNecklinesNavigation { get; set; }

    public virtual BasePantsCuff? FkBasePantsCuffsLeftNavigation { get; set; }

    public virtual BasePantsCuff? FkBasePantsCuffsRightNavigation { get; set; }

    public virtual BasePant? FkBasePantsNavigation { get; set; }

    public virtual BaseSleeveCuff? FkBaseSleeveCuffsLeftNavigation { get; set; }

    public virtual BaseSleeveCuff? FkBaseSleeveCuffsRightNavigation { get; set; }

    public virtual BaseSleeve? FkBaseSleevesNavigation { get; set; }

    public virtual BaseSweater? FkBaseSweatersNavigation { get; set; }
}
