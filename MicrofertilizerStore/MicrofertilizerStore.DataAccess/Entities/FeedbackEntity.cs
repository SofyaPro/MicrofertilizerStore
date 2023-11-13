using System.ComponentModel.DataAnnotations.Schema;

namespace MicrofertilizerStore.DataAccess.Entities
{
    [Table("feedbacks")]
    public class FeedbackEntity : BaseEntity
    {
        public double Rating { get; set; }
        public string Comment {  get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }
}
