using System.ComponentModel.DataAnnotations.Schema;

namespace MicrofertilizerStore.DataAccess.Entities
{
    [Table("admins")]
    public class AdminEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccessLevel {  get; set; }
        public string Login {  get; set; }
        public string PasswordHash { get; set; }
    }
}
