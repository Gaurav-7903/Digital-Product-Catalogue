using System.ComponentModel.DataAnnotations;

namespace Digital_Product_Catalogue.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name can' be Empty")]
        [MaxLength(50, ErrorMessage = "Product name can't be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Description can' be Empty")]
        [MaxLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is Not Provided")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Price must be Grater than 0.01")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Featured image is required.")]
        public string FeatureImage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        // Navigation Property
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductTag> Tags { get; set; }
        public ICollection<Wishlist> Wishlist { get; set; }
    }
}
