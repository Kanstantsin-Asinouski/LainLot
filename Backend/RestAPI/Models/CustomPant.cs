namespace RestAPI.Models;

public partial class CustomPant
{
    public int Id { get; set; }

    public int FkBasePants { get; set; }

    public string? CustomSettings { get; set; }
}