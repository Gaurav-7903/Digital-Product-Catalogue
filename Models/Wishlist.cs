using System.ComponentModel.DataAnnotations;

namespace Digital_Product_Catalogue.Models
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int UserId { get; set; } // Foreign Key to IdentityUser

        [Required]
        [Range(0, int.MaxValue)]
        public int ProductId { get; set; } // Foreign Key to Product

        // Navigation Property
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
