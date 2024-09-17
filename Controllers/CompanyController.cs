using Digital_Product_Catalogue.DTOs;
using Digital_Product_Catalogue.ServiceContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Digital_Product_Catalogue.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> AddCompany()
        {
            CompanyResponseDTO companyInfo =await _companyService.GetCompanyInfo();
            //if (companyInfo != null)
            //{
            //    return View(companyInfo);
            //}
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(CompanyInfoDTO companyInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(temp => temp.ErrorMessage);
                return View(companyInfoDTO);
            }
            if (companyInfoDTO == null)
            {
                return View();
            }
            CompanyResponseDTO company = await _companyService.AddCompany(companyInfoDTO);
            return RedirectToAction("Products", "Product");
        }
    }
}
