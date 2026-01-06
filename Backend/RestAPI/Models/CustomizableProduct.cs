namespace RestAPI.Models
{
    public class CustomizableProduct
    {
        public int Id { get; set; }

        public int FkCustomSportSuits { get; set; }

        public int FkFabricTypes { get; set; }

        public int FkSizeOptions { get; set; }

        public decimal Price { get; set; }

        public string? CustomizationDetails { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}