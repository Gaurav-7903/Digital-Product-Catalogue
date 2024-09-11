using System.ComponentModel.DataAnnotations;

namespace Digital_Product_Catalogue.Models
{
    public class Tag
    {
        [Key] // Primary key
        public int Id { get; set; }

        [Required(ErrorMessage = "Tag Name can't Be Empty")]
        [MaxLength(20, ErrorMessage = "Tag name can't be longer than 20 characters.")]
        [RegularExpression("^[A-Za-z0-9 ]*$")]
        public string Name { get; set; }

        // Navigation Property
        public ICollection<ProductTag> ProductTags { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
