using AutoMapper;
using Glamz.Business.Entity;
using Glamz.Business.Model;
using Glamz.Business.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Glamz.Business.Service
{
    public class AccountService : IAccountService
    {
        #region Fields
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptservice;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AccountService(IUserRepository userRepository
            , IEncryptionService encryptservice
            , IMapper mapper)
        {
            _userRepository = userRepository;
            _encryptservice = encryptservice;
            _mapper = mapper;
        }
        #endregion

        /// <summary>
        /// Request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AuthenticationResponseDto> Authenticate(AuthenticateRequestDto request)
        {
            AuthenticationResponseDto response = new AuthenticationResponseDto();
            try
            {
                User user = _mapper.Map<User>(request);

                User dbuser = await _userRepository.FindUser(x => x.IsActive && x.UserName.ToLowerInvariant() == request.UserName.ToLowerInvariant());

                if (dbuser != null)
                {
                    string decryptpassword = _encryptservice.DecryptString(dbuser.Password, _encryptservice.GetKey());
                    if (!string.IsNullOrEmpty(decryptpassword) && decryptpassword.ToLowerInvariant() == request.Password.ToLowerInvariant())
                    {
                        response = GenerateToken(dbuser);
                        response.Issuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        /// <summary>
        /// User Id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<UserDto> GetUserById(int userid)
        {
            UserDto response = new UserDto();
            try
            {
                User dbuser = await _userRepository.FindUser(x => x.IsActive && x.UserId == userid);
                if (dbuser != null)
                    response = _mapper.Map<UserDto>(dbuser);
                else
                {
                    response.ErrorMessage = ErrorMessage.USER_NOT_EXIST;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }

        #region Private Method
        public AuthenticationResponseDto GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("f4c8bbc3-5647-4dd8-9cd4-8f70dede5882");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("Name", user.UserName.ToString()),
                    new Claim("EmployeeId", user.UserId.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(TimeSpan.FromHours(10)),//_appSettings.TokenLifetime
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshTokenDto
            {
                JwtId = token.Id,
                Token = GenerateRefreshToken(),
                EmployeeId = user.UserId,
                CreatedOn = DateTime.Now,
                ExpiredOn = tokenDescriptor.Expires.Value,
            };

            return new AuthenticationResponseDto()
            {
                Issuccess = true,
                Token = tokenHandler.WriteToken(token),
                exp = tokenDescriptor.Expires.Value,
                RefreshToken = refreshToken.Token
            };
        }

        private string GenerateRefreshToken()
        {
            string token = string.Empty;
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                token = Convert.ToBase64String(randomBytes);
            }
            return token;
        }
        #endregion
    }
}
