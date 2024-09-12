using Digital_Product_Catalogue.DTOs;
using Digital_Product_Catalogue.ServiceContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Digital_Product_Catalogue.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ITagService _tagService;
        public ProductController(IProductService productService, ITagService tagService)
        {
            _productService = productService;
            _tagService = tagService;
        }

        [HttpGet]
        public IActionResult Products()
        {
            var products = _productService.GetAllProducts();
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
                return View(productRequest);
            }

            ProductResponse productResponse = await _productService.AddProduct(productRequest);

            return RedirectToAction("Products", "Product", new { area = "Admin" });
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
