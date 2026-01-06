namespace RestAPI.Models;

public class About
{
    public int Id { get; set; }

    public int FkLanguages { get; set; }

    public string Header { get; set; } = null!;

    public string Text { get; set; } = null!;
}