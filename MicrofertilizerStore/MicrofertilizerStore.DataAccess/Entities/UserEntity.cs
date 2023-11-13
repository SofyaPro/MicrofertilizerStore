using System.ComponentModel.DataAnnotations.Schema;

namespace MicrofertilizerStore.DataAccess.Entities
{
    [Table("users")]
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public string? Address { get; set; }
        public string PasswordHash {  get; set; }
        public string? CreditCardNumber { get; set; }
        public int UserType { get; set; } // 1 - индивидуальный пользователь, 2 - компания
        public string Name { get; set; } // имя компании или индивидуального пользователя
        public string PhoneNumber {  get; set; }
        public string? CEO {  get; set; }
        public string? INN { get; set; } 

        public virtual ICollection<OrderEntity>? Orders { get; set; }

        public virtual ICollection<FeedbackEntity>? Feedbacks { get; set; }

        public virtual ICollection<CartEntity>? Carts { get; set; }  
    }
}