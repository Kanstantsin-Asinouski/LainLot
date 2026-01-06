namespace RestAPI.Models;

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
}