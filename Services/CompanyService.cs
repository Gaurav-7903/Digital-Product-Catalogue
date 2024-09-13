using Digital_Product_Catalogue.DTOs;
using Digital_Product_Catalogue.ServiceContract;
using Digital_Product_Catalogue.Models;
using System.Net;
using Digital_Product_Catalogue.Data;

namespace Digital_Product_Catalogue.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;

        public CompanyService(ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<CompanyResponseDTO> AddCompany(CompanyInfoDTO companyRequest)
        {
            if (companyRequest == null)
            {
                throw new ArgumentNullException(nameof(companyRequest));
            }
            string companyLogoURL = await _productService.GetImageUrl(companyRequest.Logo);

            CompanyInfo company = new CompanyInfo()
            {
                Name = companyRequest.Name,
                Address = companyRequest.Address,
                EmailAddress = companyRequest.EmailAddress,
                Information = companyRequest.Information,
                MobileNumber = companyRequest.MobileNumber,
                Website = companyRequest.Website,
                LogoUrl = companyLogoURL,
            };
            _context.CompanyInfo.Add(company);
            _context.SaveChanges();

            return new CompanyResponseDTO()
            {
                Name = companyRequest.Name,
                Address = companyRequest.Address,
                EmailAddress = companyRequest.EmailAddress,
                Information = companyRequest.Information,
                MobileNumber = companyRequest.MobileNumber,
                Website = companyRequest.Website,
                LogoURL = companyLogoURL,
            };
        }

        public async Task<CompanyResponseDTO> GetCompanyInfo()
        {
            var companyInfo = _context.CompanyInfo.Select(company => new CompanyResponseDTO()
            {
                Name = company.Name,
                Address = company.Address,
                EmailAddress = company.EmailAddress,
                Information = company.Information,
                MobileNumber = company.MobileNumber,
                Website = company.Website,
                LogoURL = company.LogoUrl,
            }).FirstOrDefault();

            return companyInfo;
        }
    }
}
