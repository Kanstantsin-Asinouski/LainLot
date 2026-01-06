namespace RestAPI.Models;

public class Contact
{
    public int Id { get; set; }

    public int FkLanguages { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;
}