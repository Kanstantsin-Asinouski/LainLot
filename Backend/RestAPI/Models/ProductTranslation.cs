namespace RestAPI.Models
{
    public class ProductTranslation
    {
        public int Id { get; set; }

        public int FkLanguages { get; set; }

        public int FkProducts { get; set; }

        public int FkCategories { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}