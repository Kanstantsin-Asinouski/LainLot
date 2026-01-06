namespace RestAPI.Models;

public partial class CustomSleeve
{
    public int Id { get; set; }

    public int FkBaseSleeves { get; set; }

    public string? CustomSettings { get; set; }
}