using System.ComponentModel.DataAnnotations;

namespace MicrofertilizerStore.Service.Controllers.Entities
{
    public class UpdateOrderRequest
    {
        [Required]
        [MinLength(6)]
        public string Status { get; set; }

        [Required]
        [Range(1, 10000000)]
        public decimal AmountPayable { get; set; }

        [Required]
        public bool Paid { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [MinLength(10)]
        public string DeliveryAddress { get; set; }

        [Required]
        public bool Returned { get; set; }

        [Required]
        public bool PaymentMethod { get; set; }

        [MinLength(4)]
        public string BuyerName { get; set; }

        [MinLength(7)]
        [MaxLength(11)]
        public string PhoneNumber { get; set; } // для незарег. покупателя
        
        public int UserId { get; set; }
        public int DiscountId { get; set; }

    }
}
