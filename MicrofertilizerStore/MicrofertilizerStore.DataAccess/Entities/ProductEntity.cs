using System.ComponentModel.DataAnnotations.Schema;

namespace MicrofertilizerStore.DataAccess.Entities
{
    [Table("products")]
    public class ProductEntity : BaseEntity
    {
        public decimal Price { get; set; }
        public int Weight { get; set; }
        public string Manufacturer { get; set; }
        public int DiscountId { get; set; }
        public DiscountEntity? Discount { get; set; }
        public virtual ICollection<ProductEntity> Products { get; set; }
        public virtual ICollection<OrderDetailsEntity> OrderDetails { get; set; }
        public virtual ICollection<CartEntity> Carts { get; set; }
    }
}
