using System.ComponentModel.DataAnnotations;

namespace Digital_Product_Catalogue.DTOs
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class MobileNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var mobileNumber = value as string;

            // Check if the mobile number is provided
            if (string.IsNullOrEmpty(mobileNumber))
            {
                return new ValidationResult("Mobile number is required.");
            }

            // Define the mobile number pattern (example: must start with a digit and be 10 digits long)
            var regex = new Regex(@"^\d{10}$");

            if (!regex.IsMatch(mobileNumber))
            {
                return new ValidationResult("Please enter a valid 10-digit mobile number.");
            }

            return ValidationResult.Success;
        }
    }

    public class CompanyInfoDTO
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]+$")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Information { get; set; }

        [Required]
        public IFormFile Logo { get; set; }

        [Required]
        public string Address { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [MobileNumberValidation(ErrorMessage = "Invalid mobile number format.")]
        public string MobileNumber { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [Url]
        public string Website { get; set; }
    }
}
