using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Glamz.Business.Entity;

namespace Glamz.Business.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Fields
        private readonly IGlamzRepository<User> _userRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public UserRepository(IGlamzRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        /// <summary>
        /// Get User By Expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<User> FindUser(Expression<Func<User, bool>> predicate = null)
        {
            try
            {
                User response = await _userRepository.FirstOrDefaultAsync(predicate);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
