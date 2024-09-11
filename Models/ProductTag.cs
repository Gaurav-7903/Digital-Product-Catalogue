using System.ComponentModel.DataAnnotations;

namespace Digital_Product_Catalogue.Models
{
    public class ProductTag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ProductId { get; set; } // Foreign Key

        [Required]
        [Range(0, int.MaxValue)]
        public int TagId { get; set; } // Foreign Key

        // navigation property
        public Product Product { get; set; }
        public Tag Tag { get; set; }
    }
}
