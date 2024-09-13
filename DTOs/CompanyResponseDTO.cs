using System.ComponentModel.DataAnnotations;

namespace Digital_Product_Catalogue.DTOs
{
    public class CompanyResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Information { get; set; }

        public string LogoURL { get; set; }

        public string Address { get; set; }

        public string MobileNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Website { get; set; }
    }
}
