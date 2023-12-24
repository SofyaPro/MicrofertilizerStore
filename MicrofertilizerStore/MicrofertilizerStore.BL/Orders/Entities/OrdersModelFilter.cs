namespace MicrofertilizerStore.BL.Orders.Entities
{
    public class OrdersModelFilter
    {
        public string? Status { get; set; }
        public bool? Paid { get; set; }
        public bool? Returned { get; set; } 
        
    }
}
