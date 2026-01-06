namespace RestAPI.Models
{
    public class Category
    {
        public int Id { get; set; }

        public int FkLanguages { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

    }
}