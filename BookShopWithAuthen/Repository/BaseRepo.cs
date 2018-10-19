using BookShopWithAuthen.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebWithAuthentication.Repository
{

    public class BaseRepo<T> where T : class
    {
        protected ApplicationDbContext _dbContext;
        public BaseRepo()
        {
            _dbContext = new ApplicationDbContext();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Delete(T entity)
        {
            var dbSet = _dbContext.Set<T>();
            dbSet.Remove(entity);
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