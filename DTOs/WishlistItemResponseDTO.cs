namespace Digital_Product_Catalogue.DTOs
{
    public class WishlistItemResponseDTO
    {
        public int userId { get; set; }

        public IEnumerable<ProductResponse> products { get; set; }
    }
}
