using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookShopDAL
{
    public interface IBaseDAO<T> where T : class
    {
        void Dispose();
        void Create(T entity);
        void SaveChanges();
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
    }
}
