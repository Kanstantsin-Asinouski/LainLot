namespace RestAPI.Models;

public partial class CustomBelt
{
    public int Id { get; set; }

    public int FkBaseBelts { get; set; }

    public string? CustomSettings { get; set; }
}