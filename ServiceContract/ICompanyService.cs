using Digital_Product_Catalogue.DTOs;

namespace Digital_Product_Catalogue.ServiceContract
{
    public interface ICompanyService
    {
        Task<CompanyResponseDTO> AddCompany(CompanyInfoDTO companyRequest);

        Task<CompanyResponseDTO> GetCompanyInfo();
    }
}
