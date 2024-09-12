using Digital_Product_Catalogue.Data;
using Digital_Product_Catalogue.DTOs;
using Digital_Product_Catalogue.Models;
using Digital_Product_Catalogue.ServiceContract;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Digital_Product_Catalogue.Services
{
    public class ProductService : IProductService
    {

        private readonly ApplicationDbContext _context;
        private readonly ITagService _tagService;

        public ProductService(ApplicationDbContext context, ITagService tagService)
        {
            _context = context;
            _tagService = tagService;
        }

        public async Task<string> GetImageUrl(IFormFile image)
        {
            if (image == null)
            {
                return string.Empty;
            }

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
                return uniqueFileName;
            }
            return string.Empty;
        }

        public async Task<ProductResponse> AddProduct(ProductRequestDTO product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product Is not Provided");
            }
            string featureImageURL = await GetImageUrl(product.Images[product.FeaturedImageIndex]);
            Product newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                FeatureImage = featureImageURL,
            };
            // Add Product
            _context.Products.Add(newProduct);
            _context.SaveChanges();

            // Add Images
            foreach (var image in product.Images)
            {
                string ImageURl = await GetImageUrl(image);
                ProductImage productImage = new ProductImage()
                {
                    ProductId = newProduct.Id,
                    ImageURL = ImageURl,
                };
                _context.ProductImages.Add(productImage);
            }
            _context.SaveChanges();

            // Add ProductTag
            foreach (var tag in product.Tags)
            {
                ProductTag productTag = new ProductTag()
                {
                    ProductId = newProduct.Id,
                    TagId = tag
                };
                _context.ProductTags.Add(productTag);
            }
            _context.SaveChanges();

            ProductResponse productResponse = new ProductResponse()
            {
                Id = newProduct.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                FeatureImageURL = featureImageURL,
                ImagesURL = _context.ProductImages.Where(p => p.ProductId == newProduct.Id).Select(p => p.ImageURL).ToList(),
                Tags = _context.ProductTags.Where(p => p.ProductId == newProduct.Id).Include(tag => tag.Tag).Select(tag => tag.Tag).ToList(),
            };

            return productResponse;
        }

        public List<ProductResponse> GetAllProducts()
        {
            var products = _context.Products.Select(product => new ProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                FeatureImageURL = product.FeatureImage,
                ImagesURL = _context.ProductImages.Where(p => p.ProductId == product.Id).Select(p => p.ImageURL).ToList(),
                Tags = _context.ProductTags.Where(p => p.ProductId == product.Id).Include(tag => tag.Tag).Select(tag => tag.Tag).ToList(),
            }).ToList();

            return products;
        }

    }
}
