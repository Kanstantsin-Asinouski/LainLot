namespace RestAPI.Models
{
    public class FabricType
    {
        public int Id { get; set; }

        public int FkCurrencies { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}