using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glamz.Business.Model;
using Glamz.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Glamz.Business.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("GlamzPolicy")]
    public class BusinessController : ControllerBase
    {
        #region Fields
        private readonly IBusinessService _service;
        #endregion

        #region ctor
        public BusinessController(IBusinessService service)
        {
            _service = service;
        }
        #endregion

        [AllowAnonymous]
        [HttpPost("AddBusiness")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponseDto))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(CommonResponseDto))]
        public async Task<IActionResult> AddBusiness(BusinessDto model)
        {
            //BusinessDto request = new BusinessDto()
            //{
            //    BusinessId = model.BusinessId,
            //    Name = model.Name,
            //    Type = model.Type,
            //    Description = model.Description,
            //    Address = model.Address,
            //    MobileNumber = model.MobileNumber,
            //    LandlineNumber = model.LandlineNumber,
            //    FacebookLink = model.FacebookLink,
            //    TwitterLink = model.TwitterLink,
            //    WebPage = model.WebPage,
            //    InstagramLink = model.InstagramLink,
            //    IsActive = model.IsActive
            //};
            
            return Ok(await _service.AddBusiness(model));
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBusiness()
        {
            var response = await _service.GetBusiness();
            return Ok(response);
        }
    }
}
