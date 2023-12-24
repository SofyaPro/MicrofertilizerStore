namespace MicrofertilizerStore.BL.Users.Entities
{
    public class CreateUserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string PasswordHash { get; set; }
        public string? CreditCardNumber { get; set; }
        public int UserType { get; set; } // 1 - индивидуальный пользователь, 2 - компания
        public string Name { get; set; } // имя компании или индивидуального пользователя
        public string PhoneNumber { get; set; }
        public string? CEO { get; set; }
        public string? INN { get; set; }
    }
}
