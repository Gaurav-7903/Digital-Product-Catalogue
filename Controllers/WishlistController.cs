using Digital_Product_Catalogue.DTOs;
using Digital_Product_Catalogue.Models;
using Digital_Product_Catalogue.ServiceContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore.Options;
using Rotativa.AspNetCore;
using System.Security.Claims;

namespace Digital_Product_Catalogue.Controllers
{
    [Route("[controller]/[action]")]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly ICompanyService _companyService;

        public WishlistController(IWishlistService wishlistService, ICompanyService companyService)
        {
            _wishlistService = wishlistService;
            _companyService = companyService;
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
            var WishlistOfUser = await _wishlistService.GetWishlistsProductIdByUserId(userId);
            //var WishListProdutId = WishlistOfUser.Select(w => w.ProductId).ToList();

            //if (WishlistOfUser == null || !WishlistOfUser.Any())
            //{
            //    return NotFound("No wishlist items found for this user.");
            //}

            return Ok(WishlistOfUser);
        }

        [HttpGet]
        public async Task<IActionResult> WishlistItem()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            WishlistItemResponseDTO wishlistItemResponse = await _wishlistService.GetWishlistProduct(userId);
            ViewBag.CompanyInfo = await _companyService.GetCompanyInfo();
            return View(wishlistItemResponse);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadWishlist()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            WishlistItemResponseDTO wishlistItemResponse = await _wishlistService.GetWishlistProduct(userId);
            ViewBag.CompanyInfo = await _companyService.GetCompanyInfo();
            //return View(wishlistItemResponse);

            return new ViewAsPdf("DownloadWishlist", wishlistItemResponse, ViewData)
            {
                PageMargins = new Margins() { Top = 15, Bottom = 15, Left = 15, Right = 15 },
            };
        }
    }
}
