namespace MicrofertilizerStore.DataAccess.Entities
{
    public class OrderDetailsEntity : BaseEntity
    {
        public int ItemAmount { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
        public virtual ICollection<OrderDetailsEntity> OrderDetails { get; set; }
    }
}
