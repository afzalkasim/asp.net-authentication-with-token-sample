
using Glamz.Business.Model;
using Glamz.Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glamz.Business.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("GlamzPolicy")]
    public class AccountController : ControllerBase
    {
        #region Fields
        private readonly IAccountService _service;
        #endregion

        #region ctor
        public AccountController(IAccountService service)
        {
            _service = service;
        }
        #endregion


        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponseDto))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(AuthenticationResponseDto))]
        public async Task<IActionResult> Authenticate(AuthenticationDto model)
        {
            AuthenticateRequestDto request = new AuthenticateRequestDto()
            {
                UserName = model.username,
                Password = model.password
            };
            var response = await _service.Authenticate(request);
            return Ok(response);
        }

        [HttpGet("GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(UserDto))]
        public async Task<IActionResult> GetAllStaff(int userid)
        {
            return Ok(await _service.GetUserById(userid));
        }
    }
}
