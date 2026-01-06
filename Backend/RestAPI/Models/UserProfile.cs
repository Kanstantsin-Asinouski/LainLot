namespace RestAPI.Models;

public class UserProfile
{
    public int Id { get; set; }

    public int FkUsers { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? ZipPostCode { get; set; }

    public string? StateProvince { get; set; }

    public string? Country { get; set; }

    public string? Phone { get; set; }

    public string? Avatar { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}