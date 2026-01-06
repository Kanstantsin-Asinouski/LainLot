namespace DatabaseProvider.Models;

public partial class User
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

    public virtual UserRole FkUserRolesNavigation { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UserOrderHistory> UserOrderHistories { get; set; } = new List<UserOrderHistory>();

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}