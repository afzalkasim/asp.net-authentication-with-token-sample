using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Glamz.Business.Model;
using Glamz.Business.Repository;

namespace Glamz.Business.Service
{




    public class BusinessService : IBusinessService
    {

        #region Fields
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public BusinessService(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }
        #endregion

        /// <summary>
        /// Add New Business
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CommonResponseDto> AddBusiness(BusinessDto request)
        {
            CommonResponseDto response = new CommonResponseDto();
            try
            {
                Entity.Business business = _mapper.Map<Entity.Business>(request);
                await _businessRepository.AddBusiness(business);
                if (business.BusinessId > 0)
                {
                    response.Status = true;
                    response.MaxId = business.BusinessId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Delete Business
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        public async Task<CommonResponseDto> DeleteBusiness(int businessid)
        {
            CommonResponseDto response = new CommonResponseDto();
            try
            {
                Entity.Business business = await _businessRepository.FindBusiness(x => x.BusinessId == businessid);
                if (business != null && business.BusinessId > 0)
                {
                    response.Status = await _businessRepository.UpdateBusiness(business);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Get All Business
        /// </summary>
        /// <returns></returns>
        public async Task<List<BusinessDto>> GetBusiness()
        {
            List<BusinessDto> response = new List<BusinessDto>();
            try
            {
                List<Entity.Business> businesslist = await _businessRepository.FindAllBusiness(x => x.IsActive);
                if (businesslist != null & businesslist.Count > 0)
                {
                    response = _mapper.Map<List<BusinessDto>>(businesslist);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Get Business By Id
        /// </summary>
        /// <param name="businessid"></param>
        /// <returns></returns>
        public async Task<BusinessDto> GetBusinessById(int businessid)
        {
            BusinessDto response = new BusinessDto();
            try
            {
                Entity.Business dbuser = await _businessRepository.FindBusiness(x => x.IsActive && x.BusinessId == businessid);
                if (dbuser != null && dbuser.BusinessId > 0)
                {
                    response = _mapper.Map<BusinessDto>(dbuser);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// Update Existing Business
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CommonResponseDto> UpdateBusiness(BusinessDto request)
        {
            CommonResponseDto response = new CommonResponseDto();
            try
            {
                Entity.Business business = _mapper.Map<Entity.Business>(request);
                response.Status = await _businessRepository.UpdateBusiness(business);
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }
    }
}
