using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bottom_API.Data
{
    public interface IWMSRepository<T> where T: class
    {
        T FindById(object id);

        T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);


        IQueryable<T> GetAll();

        Task<bool> SaveAll();
    }
}