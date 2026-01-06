namespace Authentication.DTOs
{
    public class Registration
    {
        public required string Email { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}