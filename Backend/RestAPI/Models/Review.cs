namespace RestAPI.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int FkProducts { get; set; }

        public int FkUsers { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}