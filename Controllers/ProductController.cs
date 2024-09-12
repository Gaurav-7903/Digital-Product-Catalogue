using Microsoft.AspNetCore.Mvc;

namespace Digital_Product_Catalogue.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        public IActionResult Products()
        {
            return View();
        }
    }
}
