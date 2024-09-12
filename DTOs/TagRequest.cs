using System.ComponentModel.DataAnnotations;

namespace Digital_Product_Catalogue.DTOs
{
    public class TagRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
