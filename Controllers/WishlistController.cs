using Digital_Product_Catalogue.DTOs;
using Digital_Product_Catalogue.Models;
using Digital_Product_Catalogue.ServiceContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Digital_Product_Catalogue.Controllers
{
    [Route("[controller]/[action]")]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WishlistRequestDTO wishlistRequest)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            await _wishlistService.AddToWishlist(wishlistRequest.UserId, wishlistRequest.ProductId);
            return Json(new { message = "Product added to wishlist." });
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromBody] WishlistRequestDTO wishlistRequest)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            await _wishlistService.RemoveFromWishlist(wishlistRequest.UserId, wishlistRequest.ProductId);
            return Json(new { message = "Product Remove to wishlist." });
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlistByUserId([FromQuery] int userId = 0)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid User Id");
            }
            var WishlistOfUser = await _wishlistService.GetWishlistsByUserId(userId.ToString());
            //var WishListProdutId = WishlistOfUser.Select(w => w.ProductId).ToList();

            //if (WishlistOfUser == null || !WishlistOfUser.Any())
            //{
            //    return NotFound("No wishlist items found for this user.");
            //}

            return Ok(WishlistOfUser);
        }
    }
}
