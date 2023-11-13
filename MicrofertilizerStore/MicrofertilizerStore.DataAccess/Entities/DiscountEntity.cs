using System.ComponentModel.DataAnnotations.Schema;

namespace MicrofertilizerStore.DataAccess.Entities
{
    [Table("discounts")]
    public class DiscountEntity : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Percentage { get; set; }
        public decimal MinimalOrderAmount { get; set; }
        public virtual ICollection<OrderEntity>? Orders { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
