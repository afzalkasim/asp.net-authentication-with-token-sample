using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glamz.Business.Model;

namespace Glamz.Business.Service
{
    public interface IBusinessService
    {
        Task<List<BusinessDto>> GetBusiness();
        Task<BusinessDto> GetBusinessById(int businessid);
        Task<CommonResponseDto> AddBusiness(BusinessDto request);
        Task<CommonResponseDto> UpdateBusiness(BusinessDto request);
        Task<CommonResponseDto> DeleteBusiness(int businessid);
    }
}
