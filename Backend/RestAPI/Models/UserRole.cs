namespace RestAPI.Models;

public class UserRole
{
    public int Id { get; set; }

    public int FkAccessLevels { get; set; }

    public string Name { get; set; } = null!;
}