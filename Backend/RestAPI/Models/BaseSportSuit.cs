namespace RestAPI.Models;

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
}