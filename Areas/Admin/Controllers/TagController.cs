using Digital_Product_Catalogue.Data;
using Digital_Product_Catalogue.DTOs;
using Digital_Product_Catalogue.Models;
using Digital_Product_Catalogue.ServiceContract;
using Digital_Product_Catalogue.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Digital_Product_Catalogue.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            //var tags = _tagService.GetAllTags();
            return View();
        }

        [HttpPost]
        public IActionResult AddTag([FromBody] TagRequest tagRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(temp => temp.ErrorMessage);
                return View(tagRequest);
            }

            if (tagRequest == null)
            {
                return View();
            }

            Tag tag = _tagService.AddTag(tagRequest.Name);
            return Ok(new { message = "Tag added successfully", tag });
            //return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
    }
}
