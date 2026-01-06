namespace RestAPI.Models;

public partial class CustomSweater
{
    public int Id { get; set; }

    public int FkBaseSweaters { get; set; }

    public string? CustomSettings { get; set; }
}