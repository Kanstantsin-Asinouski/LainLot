namespace RestAPI.Models;

public class User
{
    public int Id { get; set; }

    public int FkUserRoles { get; set; }

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool ConfirmEmail { get; set; }

    public string ConfirmationToken { get; set; }

    public DateTime? ConfirmationTokenExpires { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}