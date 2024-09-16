// Ignore Spelling: Wishlist

using Digital_Product_Catalogue.Data;
using Digital_Product_Catalogue.DTOs;
using Digital_Product_Catalogue.Models;
using Digital_Product_Catalogue.ServiceContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Digital_Product_Catalogue.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishlistService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddToWishlist(int userId, int productId)
        {

            Wishlist wishlistItem = new Wishlist
            {
                UserId = userId,
                ProductId = productId
            };
            Console.WriteLine(wishlistItem.Id);

            _context.Wishlists.Add(wishlistItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromWishlist(int userId, int productId)
        {
            var wishlistItem = await _context.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
            Console.WriteLine(wishlistItem.Id);

            if (wishlistItem != null)
            {
                _context.Wishlists.Remove(wishlistItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Wishlist>> GetWishlistsProductIdByUserId(int userId)
        {
            var userRecord = await _userManager.FindByIdAsync(userId.ToString());

            if (userRecord == null)
            {
                throw new Exception("User Not Found");
            }

            var WishlistProductItemIdOfUser = await _context.Wishlists.Where(w => w.UserId == userRecord.Id).ToListAsync();

            return WishlistProductItemIdOfUser;
        }

        public async Task<WishlistItemResponseDTO> GetWishlistProduct(int userId)
        {
            var userRecord = await _userManager.FindByIdAsync(userId.ToString());

            if (userRecord == null)
            {
                throw new Exception("User Not Found");
            }

            var WishlistProductOfUser = new WishlistItemResponseDTO()
            {
                userId = userId,
                products = _context.Wishlists.Where(w => w.UserId == userRecord.Id).Include(pid => pid.Product).ThenInclude(product => product.Tags).Select(product => new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Product.Name,
                    Description = product.Product.Description,
                    Price = product.Product.Price,
                    FeatureImageURL = product.Product.FeatureImage,
                    ImagesURL = product.Product.Images.Select(pi => pi.ImageURL).ToList(),
                    Tags = product.Product.Tags.Select(pt => pt.Tag).ToList(),
                }).ToList(),
            };

            return WishlistProductOfUser;

        }
    }
}
