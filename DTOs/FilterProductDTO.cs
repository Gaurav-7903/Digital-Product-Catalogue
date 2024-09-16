namespace Digital_Product_Catalogue.DTOs
{
    public class FilterProductDTO
    {
        public string? SearchText { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public List<int>? TagList { get; set; }
    }
}
