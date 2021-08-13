using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Glamz.Business.Entity;

namespace Glamz.Business.Repository
{
    public interface IBusinessRepository
    {
        Task<Entity.Business> FindBusiness(Expression<Func<Entity.Business, bool>> predicate = null);
        Task<List<Entity.Business>> FindAllBusiness(Expression<Func<Entity.Business, bool>> predicate = null);
        Task<Glamz.Business.Entity.Business> AddBusiness(Glamz.Business.Entity.Business addrequest);
        Task<bool> UpdateBusiness(Glamz.Business.Entity.Business addrequest);
        Task<bool> DeleteBusiness(int businessid);
    }
}
