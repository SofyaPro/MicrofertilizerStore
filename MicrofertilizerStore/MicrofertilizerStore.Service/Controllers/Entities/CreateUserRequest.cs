﻿using System.ComponentModel.DataAnnotations;

namespace MicrofertilizerStore.Service.Controllers.Entities
{
    public class CreateUserRequest : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string numbers = "0123456789";
            var errors = new List<ValidationResult>();

            if (!Email.Contains('@'))
            {
                errors.Add(new ValidationResult("Wrong email format."));
            }
            if (!PhoneNumber.All(x => numbers.Contains(x)))
            {
                errors.Add(new ValidationResult("Phone number wrong format."));
            }
            if (!CreditCardNumber.All(x => numbers.Contains(x)))
            {
                errors.Add(new ValidationResult("Wrong credit card number format."));
            }
            return errors;
        }
    }
}
