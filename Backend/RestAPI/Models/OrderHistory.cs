namespace RestAPI.Models
{
    public class OrderHistory
    {
        public int Id { get; set; }

        public int FkOrders { get; set; }

        public int FkOrderStatuses { get; set; }

        public DateTime ChangedAt { get; set; }
    }
}