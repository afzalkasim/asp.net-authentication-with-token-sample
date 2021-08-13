using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Glamz.Business.Entity;

namespace Glamz.Business.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
        #region Fields
        private readonly IGlamzRepository<Glamz.Business.Entity.Business> _businessRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public BusinessRepository(IGlamzRepository<Glamz.Business.Entity.Business> businessRepository)
        {
            _businessRepository = businessRepository;
        }
        #endregion

        /// <summary>
        /// Add New Business
        /// </summary>
        /// <param name="addrequest"></param>
        /// <returns></returns>
        public async Task<Entity.Business> AddBusiness(Entity.Business addrequest)
        {
            try
            {
                return await _businessRepository.InsertAsync(addrequest);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Business
        /// </summary>
        /// <param name="buinessid"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBusiness(int buinessid)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Find Business
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<Entity.Business> FindBusiness(Expression<Func<Entity.Business, bool>> predicate = null)
        {
            try
            {
                return await _businessRepository.FirstOrDefaultAsync(x => x.IsActive);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Find All Business
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<Entity.Business>> FindAllBusiness(Expression<Func<Entity.Business, bool>> predicate = null)
        {
            try
            {
                return await _businessRepository.GetAllAsync(predicate);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Existing Business
        /// </summary>
        /// <param name="addrequest"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBusiness(Entity.Business addrequest)
        {
            try
            {
                await _businessRepository.UpdateAsync(addrequest);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
