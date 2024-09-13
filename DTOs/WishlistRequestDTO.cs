using System.ComponentModel.DataAnnotations;

namespace Digital_Product_Catalogue.DTOs
{
    public class WishlistRequestDTO
    {
        [Required]
        [Range(1 , int.MaxValue)]
        public int UserId { set; get; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { set; get; }
    }
}
