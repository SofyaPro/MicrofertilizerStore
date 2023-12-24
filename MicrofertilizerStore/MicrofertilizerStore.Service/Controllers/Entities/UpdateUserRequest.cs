using System.ComponentModel.DataAnnotations;

namespace MicrofertilizerStore.Service.Controllers.Entities
{
    public class UpdateUserRequest
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [MinLength(10)]
        public string Address { get; set; }

        [Required]
        [Range(1, 2)]
        public int UserType { get; set; }

        [Required]
        [MinLength(7)]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(10)]
        public string Email { get; set; }

        [MinLength(16)]
        [MaxLength(16)]
        public string CreditCardNumber { get; set; }

        [MaxLength(5)]
        public string CEO { get; set; }


        [MinLength(16)]
        [MaxLength(16)]
        public string INN { get; set; }
    }
}
