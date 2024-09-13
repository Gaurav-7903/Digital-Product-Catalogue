
using Digital_Product_Catalogue.Models;

namespace Digital_Product_Catalogue.ServiceContract
{
    public interface IWishlistService
    {
        Task AddToWishlist(int userId, int productId);
        Task RemoveFromWishlist(int userId, int productId);

        Task<IEnumerable<Wishlist>> GetWishlistsByUserId(string userId);
    }
}
