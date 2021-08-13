using Glamz.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Glamz.Business.Repository
{
    public interface IUserRepository
    {
        Task<User> FindUser(Expression<Func<User, bool>> predicate = null);
    }
}
