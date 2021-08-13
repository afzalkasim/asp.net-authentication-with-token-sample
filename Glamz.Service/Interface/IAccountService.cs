using Glamz.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Glamz.Business.Service
{
    public interface IAccountService
    {
        Task<AuthenticationResponseDto> Authenticate(AuthenticateRequestDto request);
        Task<UserDto> GetUserById(int userid);
    }
}
