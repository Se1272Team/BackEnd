using BookShopBOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookShopDAL
{
    public class BaseDAO<T> : IBaseDAO<T> where T : class
    {
        protected MyDbContext _dbContext;
        public BaseDAO()
        {
            _dbContext = new MyDbContext();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Create(T entity)
        {
            var dbSet = _dbContext.Set<T>();
            dbSet.Add(entity);

        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public IQueryable<T> Get()
        {
            var dbSet = _dbContext.Set<T>();
            var result = Queryable.AsQueryable<T>(dbSet);
            return result;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            var dbSet = _dbContext.Set<T>();
            var result = Queryable.Where<T>(dbSet, predicate);
            return result;
        }
    }
}
