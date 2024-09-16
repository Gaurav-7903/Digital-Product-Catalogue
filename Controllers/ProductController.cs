using Digital_Product_Catalogue.DTOs;
using Digital_Product_Catalogue.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Digital_Product_Catalogue.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWishlistService _wishlistService;
        private readonly ITagService _tagService;
        public ProductController(IProductService productService, ITagService tagService, IWishlistService wishlistService)
        {
            _productService = productService;
            _tagService = tagService;
            _wishlistService = wishlistService;
        }

        [HttpGet]
        public IActionResult Products()
        {
            var products = _productService.GetAllProducts();
            ViewBag.Tags = _tagService.GetAllTags();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var tags = _tagService.GetAllTags();
            ViewBag.Tags = tags;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductRequestDTO productRequest)
        {

            if (!ModelState.IsValid)
            {
                var tags = _tagService.GetAllTags();
                ViewBag.Tags = tags;
                return View(productRequest);
            }

            ProductResponse productResponse = await _productService.AddProduct(productRequest);

            return RedirectToAction("Products", "Product");
        }

        [HttpGet]
        public IActionResult GetProductById([FromQuery] int productId)
        {
            if (productId <= 0)
            {
                return BadRequest("Product Not Found");
            }

            ProductResponse product = _productService.GetProductById(productId);
            return Ok(new { product });
        }

        // Filter Product Data 
        public IActionResult FilterProducts([FromBody] FilterProductDTO filterProduct)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            IEnumerable<ProductResponse> filteredProducts = _productService.GetFilteredProducts(filterProduct.SearchText, filterProduct.MinPrice, filterProduct.MaxPrice, filterProduct.TagList);

            return PartialView("_ProductList", filteredProducts);
        }

            

        public async Task<List<string>> GetImagesURL(List<IFormFile> Images)
        {
            var imageUrls = new List<string>();

            if (Images != null && Images.Count > 0)
            {
                for (int i = 0; i < Images.Count; i++)
                {
                    var image = Images[i];
                    if (image.Length > 0)
                    {
                        // Generate unique file name and save the image
                        var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                        var extension = Path.GetExtension(image.FileName);
                        var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products", uniqueFileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        // Add the image URL to list
                        imageUrls.Add($"{uniqueFileName}");
                    }
                }
            }
            return imageUrls;
        }
    }
}
