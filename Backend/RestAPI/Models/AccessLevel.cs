namespace RestAPI.Models;

public class AccessLevel
{
    public int Id { get; set; }

    public int Level { get; set; }

    public string Description { get; set; } = null!;
}