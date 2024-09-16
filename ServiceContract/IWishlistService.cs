
using Digital_Product_Catalogue.DTOs;
using Digital_Product_Catalogue.Models;

namespace Digital_Product_Catalogue.ServiceContract
{
    public interface IWishlistService
    {
        Task AddToWishlist(int userId, int productId);
        Task RemoveFromWishlist(int userId, int productId);

        Task<IEnumerable<Wishlist>> GetWishlistsProductIdByUserId(int userId);
        Task<WishlistItemResponseDTO> GetWishlistProduct(int userId);
    }
}
