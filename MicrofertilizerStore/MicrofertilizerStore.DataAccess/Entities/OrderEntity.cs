using System.ComponentModel.DataAnnotations.Schema;

namespace MicrofertilizerStore.DataAccess.Entities
{
    [Table("orders")]
    public class OrderEntity : BaseEntity
    {
        public string Status { get; set; }
        public decimal AmountPayable { get; set; }
        public bool Paid { get; set; } // true - yes, false - no
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Returned { get; set; } // true - yes, false - no
        public bool PaymentMethod { get; set; } // true - карта, false - наличка при доставке
        public string? BuyerName { get; set; } // для незарег. покупателя (иначе через UserId)
        public string? PhoneNumber { get; set; } // для незарег. покупателя
        public int? UserId { get; set; }
        public UserEntity? User { get; set; }
        public int? DiscountId { get; set; }
        public DiscountEntity? Discount { get; set; }
        public virtual ICollection<OrderDetailsEntity> OrderDetails { get; set; }
    }
}