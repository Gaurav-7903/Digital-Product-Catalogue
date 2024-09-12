using Digital_Product_Catalogue.DTOs;

namespace Digital_Product_Catalogue.ServiceContract
{
    public interface IProductService
    {
        Task<string> GetImageUrl(IFormFile image);
        Task<ProductResponse> AddProduct(ProductRequestDTO product);

        List<ProductResponse> GetAllProducts();
    }
}
