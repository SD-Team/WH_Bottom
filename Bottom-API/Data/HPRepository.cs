using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ME_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Bottom_API.Data
{
    public class HPRepository<T> : IHPRepository<T> where T : class
    {
        private readonly HP_DataContext _context;
        public HPRepository(HP_DataContext context)
        {
            _context =  context;
        }
        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null) {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.AsQueryable();
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate).AsQueryable();
        }

        public T FindById(object id)
        {
            return _context.Set<T>().Find(id);
        }

        public T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return FindAll(includeProperties).SingleOrDefault(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}