using MicrofertilizerStore.DataAccess.Entities;

namespace MicrofertilizerStore.BL.Orders.Entities
{
    public class CreateOrderModel
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public decimal AmountPayable { get; set; }
        public bool Paid { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Returned { get; set; }
        public bool PaymentMethod { get; set; } // true - карта, false - наличка при доставке
        public string? BuyerName { get; set; } // для незарег. покупателя (иначе через UserId)
        public string? PhoneNumber { get; set; }
        public int? UserId { get; set; }
        public int? DiscountId { get; set; }
    }
}
