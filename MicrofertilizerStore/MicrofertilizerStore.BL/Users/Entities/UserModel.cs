namespace MicrofertilizerStore.BL.Users.Entities
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string BankAccount { get; set; }
        public int UserType { get; set; } // 1 - индивидуальный пользователь, 2 - компания
    }
}
