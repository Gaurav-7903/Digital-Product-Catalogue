using Digital_Product_Catalogue.ServiceContract;
using Microsoft.AspNetCore.Mvc;

namespace Digital_Product_Catalogue.ViewModel
{
    public class CompanyInfoViewComponent : ViewComponent
    {
        private readonly ICompanyService _companyService;

        public CompanyInfoViewComponent(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string viewName = "Default")
        {
            var companyInfo = await _companyService.GetCompanyInfo();

            switch (viewName)
            {
                case "Logo":
                    return View("Logo", companyInfo);
                case "Name":
                    return View("Name", companyInfo);
                case "Footer":
                    return View("Footer", companyInfo);
                default:
                    return View("Default", companyInfo);

            }
        }
    }
}
