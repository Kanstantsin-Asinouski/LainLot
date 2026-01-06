namespace RestAPI.Models
{
    public class UserOrderHistory
    {
        public int Id { get; set; }

        public int FkOrders { get; set; }

        public int FkUsers { get; set; }

    }
}