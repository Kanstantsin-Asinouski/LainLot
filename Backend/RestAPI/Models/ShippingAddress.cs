namespace RestAPI.Models
{
    public class ShippingAddress
    {
        public int Id { get; set; }

        public int FkCountries { get; set; }

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string ZipPostCode { get; set; } = null!;

        public string StateProvince { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}