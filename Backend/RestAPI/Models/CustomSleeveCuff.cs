namespace RestAPI.Models;

public partial class CustomSleeveCuff
{
    public int Id { get; set; }

    public int FkBaseSleeveCuffs { get; set; }

    public string? CustomSettings { get; set; }
}