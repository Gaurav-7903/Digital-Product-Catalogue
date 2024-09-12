using Digital_Product_Catalogue.Models;

namespace Digital_Product_Catalogue.DTOs
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string FeatureImageURL { get; set; }
        public List<string> ImagesURL { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
