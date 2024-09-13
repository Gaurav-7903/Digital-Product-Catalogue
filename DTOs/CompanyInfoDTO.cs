using System.ComponentModel.DataAnnotations;

namespace Digital_Product_Catalogue.DTOs
{
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

        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [Url]
        public string Website { get; set; }
    }
}
