namespace RestAPI.Models;

public class Language
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Abbreviation { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string DateFormat { get; set; } = null!;

    public string TimeFormat { get; set; } = null!;
}