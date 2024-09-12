using Digital_Product_Catalogue.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Digital_Product_Catalogue.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Fixed decimal range
            builder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");


            builder.Entity<Product>().ToTable(nameof(Product));
            builder.Entity<Tag>().ToTable(nameof(Tag));
            builder.Entity<ProductTag>().ToTable(nameof(ProductTag));
            builder.Entity<ProductImage>().ToTable(nameof(ProductImage));
            builder.Entity<Wishlist>().ToTable(nameof(Wishlist));
        }

    }
}
