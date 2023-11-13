namespace MicrofertilizerStore.DataAccess.Entities
{
    public class CartEntity : BaseEntity
    {
        public decimal TotalPriceAmount { get; set; }
        public int ProductsAmount { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }
}
