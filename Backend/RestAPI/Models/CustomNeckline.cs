namespace RestAPI.Models;

public partial class CustomNeckline
{
    public int Id { get; set; }

    public int FkBaseNecklines { get; set; }

    public string? CustomSettings { get; set; }
}