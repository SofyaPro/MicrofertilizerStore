namespace MicrofertilizerStore.Service.Controllers.Entities
{
    public class OrdersFilter
    {
        public string? Status { get; set; }
        public bool? Paid { get; set; }
        public bool? Returned { get; set; }
    }
}
