using Microsoft.AspNetCore.Identity;

namespace Digital_Product_Catalogue.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? PersonName { get; set; }
    }
}
