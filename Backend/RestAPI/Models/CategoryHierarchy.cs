namespace RestAPI.Models
{
    public class CategoryHierarchy
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int FkCategories { get; set; }
    }
}