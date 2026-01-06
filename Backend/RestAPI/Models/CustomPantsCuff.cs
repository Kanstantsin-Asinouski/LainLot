namespace RestAPI.Models;

public partial class CustomPantsCuff
{
    public int Id { get; set; }

    public int FkBasePantCuffs { get; set; }

    public string? CustomSettings { get; set; }
}