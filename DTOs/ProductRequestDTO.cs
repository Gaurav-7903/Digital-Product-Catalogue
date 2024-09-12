using System.ComponentModel.DataAnnotations;

namespace Digital_Product_Catalogue.DTOs
{
    public class ProductRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public List<int> Tags { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }

        [Required]
        public int FeaturedImageIndex { get; set; }

    }
}
